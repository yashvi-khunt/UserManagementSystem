
using LS.BLL.Interfaces;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPut("edit/{email}")]
        public async Task<IActionResult> EditUserDetails(string email, VMUpdateUser newModel)
        {
            var response  = await _userService.EditUserDetails(email, newModel);

            return StatusCode(response.Success ? 200 : 401, response);
        }

        
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] VMChangePassword model)
        {
            var response = await _userService.ChangePassword(model);

            return StatusCode(response.Success ? 200 : 401, response);
        }


        [HttpGet("details/{email}")]
        public async Task<IActionResult> GetUserDetails(string email)
        {
            var response = await _userService.GetUserDetails(email);

            return StatusCode(response.Success ? 200 : 401, response);
        }

    }
}
