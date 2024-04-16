using Alms.DAL.ViewModels;
using AutoMapper;
using LS.BLL.Repositories;
using LS.BLL.SQLRepository;
using LS.DAL.Helper;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IProcedureManager _procedureManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IConfiguration configuration, LoginDbContext context, IEmailService service, IProcedureManager procedureManager,IMapper mapper) 
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = service;
            _procedureManager = procedureManager;
            _mapper = mapper;
        }

         [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            var user = await _userManager.GetUserAsync(User);
            var userDetails = new VMUserDetails()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return Ok(userDetails);
        }

        
        [HttpPut("edit")]
        public async Task<IActionResult> EditUserDetails(VMUpdateUser newModel)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
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


        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] VMChangePassword model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return Ok(new Response("Password Changed successfully."));
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong.");
                    }

                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong.", false));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(ex.Message, false));
            }
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<VMGetUserList>>> GetAllUsersAsync([FromQuery]GetUsersInputModel getUsersInputModel)
        {

            var spParameters = new GetUsersInputModel
            {

                Page = getUsersInputModel.Page,
                PageSize = getUsersInputModel.PageSize,
                Field = getUsersInputModel.Field,
                Sort = getUsersInputModel.Sort,
                Text = getUsersInputModel.Text ?? "",
                ToDate = getUsersInputModel.ToDate ?? "",
                FromDate = getUsersInputModel.FromDate ?? "",

            };

            var users = _procedureManager.ExecStoreProcedureMulResults<StoredProcedureCommonModel, VmSPGetUsers>(StoredProcedure.GetUsers, spParameters);

            var countData = users.Item1[0].Count;
            var employeesData = users.Item2;


            VMGetUserList getEmployees = new VMGetUserList
            {
                Count = (int)countData,
                Users = employeesData
            };

            var mappedEmployees = _mapper.Map<VMGetUserList> (getEmployees);
            if (mappedEmployees == null)
            {
                return NotFound(new Response("Data not found.",false));
            }
            return Ok(new Response<VMGetUserList>(mappedEmployees,true,"Data loaded successfully!"));

        }


    }
}
