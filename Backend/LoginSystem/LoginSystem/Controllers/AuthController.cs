using LS.BLL.Services;
using LS.DAL.Helper;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public async Task<IActionResult> Login([FromBody] LoginModel model
            )
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (!user.EmailConfirmed)
            {
                // User's email is not confirmed
                return StatusCode(StatusCodes.Status403Forbidden, new Response("Email is not confirmed. Please confirm your email before signing in.", false));
            }

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                // Get the JWT secret key and token validity duration from configuration
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

                // Create a JWT token
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),

                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                // Return a successful response with the generated token and its expiration
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new Response("Username or Password is incorrect", false));
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null) return StatusCode(StatusCodes.Status500InternalServerError, new Response("User with same email already exists.", false));


            //ApplicationUser user = new ApplicationUser()
            //{
            //    UserName = model.Email,
            //    Email = model.Email,
            //    SecurityStamp = Guid.NewGuid().ToString(),
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

        //[HttpPost("SendMail")]
        //public async Task<IActionResult> SendMail()
        //{
        //    try
        //    {
        //        MailRequest mailRequest = new MailRequest()
        //        {
        //            RecipientEmail = "yashvikhunt02@gmail.com",
        //            Subject = "Testing SendMail function",
        //            Body = "Hello, this is a testing mail. Thanks for joining.",
        //        };

        //        await _emailService.SendEmailAsync(mailRequest);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw; 
        //    }
        //}


        [HttpGet("confirmUserEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if (userId == null || token == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response("User or token not found", false));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response("User not found", false));

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Redirect("http://localhost:5173/");
                
                //return StatusCode(StatusCodes.Status200OK, new Response("UserConfirmed", true));
            }
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong", false));
        }

    }
}