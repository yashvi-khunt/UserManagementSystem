using LS.BLL.Repositories;
using LS.DAL.Helper;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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
                // Create a JWT token
                var token = GenerateJwtToken(user);

                // Return a successful response with the generated token and its expiration
                return Ok(new { token });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response("Username or Password is incorrect", false));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] VMRegister model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null) return StatusCode(StatusCodes.Status500InternalServerError, new Response("User with same email already exists.", false));


            //ApplicationUser user = new ApplicationUser()
            //{
            //    UserName = model.Email,
            //    Email = model.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //    EmailConfirmed = false,
            //};

            //var result = await _userManager.CreateAsync(user, model.Password);

            //if (!result.Succeeded)
            //{
            //    var message = String.Empty;
            //    foreach (var error in result.Errors)
            //    {
            //        message += error.Description;
            //    }
            //    return StatusCode(StatusCodes.Status500InternalServerError, new Response(message, false));
            //}

            try
            {
                //temporary
                var user = await _userManager.FindByEmailAsync("user@example.com");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

                MailRequest mailRequest = new MailRequest()
                {
                    RecipientEmail = model.Email,
                    Subject = "Confirmation Mail",
                    Body = $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.",
                };

                await _emailService.SendEmailAsync(mailRequest);
                return Ok(new Response("User created successfully!"));
            }
            catch (Exception ex)
            {
                throw;
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
                return Redirect("http://localhost:5173/");
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong", false));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] VMForgotPassword model)
        {
            //var user = await _userManager.FindByEmailAsync(model.Email);
            var user = await _userManager.FindByEmailAsync("user@example.com");
            if (user == null) return StatusCode(StatusCodes.Status202Accepted, new Response("Sent Email to user", false));

            try
            {

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var resetLink = $"http://localhost:5173/auth/reset-password?userEmail={user.Email}&token={token}";

                MailRequest mailRequest = new MailRequest()
                {
                    RecipientEmail = model.Email,
                    Subject = "Password Reset Mail",
                    Body = $"Please click on the following link to reset password.<a href='{resetLink}'>here</a>.",
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

               if(result.Succeeded) return Ok(new Response("Password Updated Successfully", true));

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

        private string GenerateJwtToken(ApplicationUser user)
        {
            // Get the JWT secret key and token validity duration from configuration
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:TokenValidityInMinutes"])),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
