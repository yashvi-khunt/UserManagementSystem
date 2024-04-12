using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LS.BLL.Interfaces;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace LS.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<Response<string>> Login(VMLogin model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return new Response<string>("Username or Password is incorrect", false);
            }

            if (!user.EmailConfirmed)
            {
                return new Response<string>("Email is not confirmed. Please confirm your email before signing in.", false);
            }

            var authClaims = new List<Claim> { new(ClaimTypes.Email, user.Email) };

            var token = GenerateJwtToken(authClaims);

            return new Response<string>(token);
        }
        public async Task<Response<ApplicationUser>> Register(VMRegister model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                return new Response<ApplicationUser>("User with the same email already exists.", false);
            }

            ApplicationUser user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return new Response<ApplicationUser>(result.Errors.Select(e => e.Description).Aggregate((acc, err) => acc + ", " + err), false);
            }
            else
            {
                return new Response<ApplicationUser>(user, true, message: "User created successfully!");
            }
        }

        public async Task<Response> ConfirmEmail(VMConfirmEmail model)
        {
            if (model.UserId == null || model.Token == null)
                return new Response("User or token not found", false);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return new Response("User not found", false);

            var result = await _userManager.ConfirmEmailAsync(user, model.Token);
            if (result.Succeeded)
            {
                return new Response { Success = true };
            }
            else
                return new Response("Something went wrong", false);
        }


        public async Task<Response<ApplicationUser>> ForgotPassword(VMForgotPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return new Response<ApplicationUser>("Sent Email to user", false);

            return new Response<ApplicationUser>(user, true, "Email sent successfully!");
        }
        public async Task<Response> ResetPassword(VMResetPassword model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

                if (result.Succeeded) return new Response("Password Updated Successfully", true);

                if (!result.Succeeded)
                {
                    var message = String.Empty;
                    foreach (var error in result.Errors)
                    {
                        message += error.Description;
                    }
                    return new Response(message, false);
                }

            }

            return new Response("Something went wrong, Please try again.", false);
        }


        private string GenerateJwtToken(List<Claim> claims)
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
