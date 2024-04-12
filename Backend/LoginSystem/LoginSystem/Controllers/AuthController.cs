using LS.BLL.Interfaces;
using LS.Core.Helpers;
using LS.DAL.Helper;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;

        public AuthController(UserManager<ApplicationUser> userManager, IAuthService authService, IEmailService service)
        {
            _userManager = userManager;
            _authService = authService;
            _emailService = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] VMLogin model
            )
        {
            var response = await _authService.Login(model);
            return StatusCode(response.Success ? 200 : 401, response);
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] VMRegister model)
        {
            var response = await _authService.Register(model);
            if (response.Success)
            {
                var user = response.Data;
                try
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, token = token }, Request.Scheme);

                    var emailTemplate = EmailTemplate.UserConfirmationMail(user.UserName, confirmationLink);

                    MailRequest mailRequest = new MailRequest()
                    {
                        RecipientEmail = model.Email,
                        Subject = "Confirmation Mail",
                        Body = emailTemplate,
                    };

                    await _emailService.SendEmailAsync(mailRequest);
                    return Ok(new Response("User created successfully!", true));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response("Failed to register user. Please try again later.", false));
                }
            }
            return StatusCode(response.Success ? 200 : 500, response);
        }


        [HttpGet("confirmUserEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] VMConfirmEmail model)
        {
            var response = await _authService.ConfirmEmail(model);
            if (response.Success) return Redirect("http://localhost:5173/auth/confirm-email");
            return StatusCode(401, response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] VMForgotPassword model)
        {
            var response = await _authService.ForgotPassword(model);
            if (response.Success)
            {
                try
                {
                    var user = response.Data;
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var resetLink = $"http://localhost:5173/auth/reset-password?userEmail={user.Email}&token={token}";

                    var emailTemplate = EmailTemplate.PasswordResetMail(user.UserName, resetLink);

                    MailRequest mailRequest = new MailRequest()
                    {
                        RecipientEmail = model.Email,
                        Subject = "Password Reset Mail",
                        Body = emailTemplate,
                    };

                    await _emailService.SendEmailAsync(mailRequest);
                    return Ok(new Response("Mail sent successfully!", true));
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response("Failed to send password reset mail. Please try again later.", false));
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new Response("Failed to send password reset mail. Please try again later.", false));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] VMResetPassword model)
        {

            var response = await _authService.ResetPassword(model);
            return StatusCode(response.Success ? 200 : 401, response);

        }

    }
}
