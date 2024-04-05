using LS.BLL.Repositories;
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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] VMLogin model)
        {
            var result = await _authService.Login(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] VMRegister model)
        {
            var result = await _authService.Register(model);

            return Ok(result);
        }

        [HttpGet("confirmUserEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery]VMConfirmEmail model)
        {
            var result = await _authService.ConfirmEmail(model);
            if (!result.Success)
            {
                return Ok(result);

            }
            else
            {
                return Redirect("http://localhost:5173/");
            }
        }

    }
}