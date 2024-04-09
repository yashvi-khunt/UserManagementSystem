using LS.BLL.Repositories;
using LS.DAL.Helper;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using System.ComponentModel;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Xml;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        private LoginDbContext _context { get; set; }

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, LoginDbContext context, IEmailService service)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _emailService = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] VMLogin model
            )
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response("Username or Password is incorrect", false));
            }

            if (!user.EmailConfirmed)
            {
                // User's email is not confirmed
                return StatusCode(StatusCodes.Status403Forbidden, new Response("Email is not confirmed. Please confirm your email before signing in.", false));
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var authClaims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };


                // Create a JWT token
                var token = GenerateJwtToken(user, authClaims);

                // Return a successful response with the generated token and its expiration
                return Ok(new { token });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response("Username or Password is incorrect", false));
            }
        }

        [Authorize]
        [HttpGet("details/{email}")]
        public async Task<IActionResult> GetUserDetails(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userDetails = new VMUserDetails()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return Ok(userDetails);
        }

        [Authorize]
        [HttpPut("edit/{email}")]
        public async Task<IActionResult> EditUserDetails(string email, VMUpdateUser newModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (newModel.FirstName != null) user.FirstName = newModel.FirstName;
                if (newModel.LastName != null) user.LastName = newModel.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Ok(new Response("User updated successfully", true));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong.", false));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong.", false));
            }

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] VMRegister model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null) return StatusCode(StatusCodes.Status500InternalServerError, new Response("User with same email already exists.", false));


            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var message = String.Empty;
                foreach (var error in result.Errors)
                {
                    message += error.Description;
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(message, false));
            }

            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

                // Constructing the email template
                var emailTemplate = $@"
                    <html>
                    <head>
                        <title>Confirmation Mail</title>
                    </head>
                    <body>
                        <p>Hi {user.UserName},</p>
                        <p>In order to start using your new account, you need to confirm your email address.</p>
                        <p>Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.</p>
                        <p>If you did not sign up for this account, you can ignore this email and the account will be deleted.</p>
                    </body>
                    </html>";

                MailRequest mailRequest = new MailRequest()
                {
                    RecipientEmail = model.Email,
                    Subject = "Confirmation Mail",
                    Body = emailTemplate,
                };

                await _emailService.SendEmailAsync(mailRequest);
                return Ok(new Response("User created successfully!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Failed to register user. Please try again later.", false));
            }
        }


        [HttpGet("confirmUserEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] VMConfirmEmail model)
        {
            if (model.UserId == null || model.Token == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response("User or token not found", false));

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response("User not found", false));

            //add a step to verify token

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (result.Succeeded)
            {
                return Redirect("http://localhost:5173/auth/confirm-email");
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong", false));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] VMForgotPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return StatusCode(StatusCodes.Status202Accepted, new Response("Sent Email to user", false));

            

            try
            {

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetLink = $"http://localhost:5173/auth/reset-password?userEmail={user.Email}&token={token}";

                var emailTemplate = $@"
                    <html>
                    <head>
                        <title>Password Reset Mail</title>
                    </head>
                    <body>
                        <p>Hi {user.UserName},</p>
                        <p>In order to change your password, you need to click on the below link.</p>
                        <p>This link will redirect you to the password reset page <a href='{resetLink}'>Click here</a>.</p>
                        <p>If you did not sign up for this account, you can ignore this email and the account will be deleted.</p>
                    </body>
                    </html>";

                MailRequest mailRequest = new MailRequest()
                {
                    RecipientEmail = model.Email,
                    Subject = "Password Reset Mail",
                    Body = emailTemplate,
                };

                await _emailService.SendEmailAsync(mailRequest);
                return Ok(new Response("Sent password reset mail successfully!"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] VMResetPassword model)
        {

            //add token expiry code

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (result.Succeeded) return Ok(new Response("Password Updated Successfully", true));

                if (!result.Succeeded)
                {
                    var message = String.Empty;
                    foreach (var error in result.Errors)
                    {
                        message += error.Description;
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response(message, false));
                }

            }

            return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong, Please try again.", false));

        }

        private string GenerateJwtToken(ApplicationUser user, List<Claim> claims)
        {
            // Get the JWT secret key and token validity duration from configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
