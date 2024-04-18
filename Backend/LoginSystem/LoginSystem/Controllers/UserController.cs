using Alms.DAL.ViewModels;
using AutoMapper;
using LS.BLL.Repositories;
using LS.BLL.SQLRepository;
using LS.Core.Helpers;
using LS.DAL.Helper;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoginSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IProcedureManager _procedureManager;
        private readonly IMapper _mapper;


        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, LoginDbContext context, IEmailService service, IProcedureManager procedureManager, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = service;
            _procedureManager = procedureManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails([FromQuery] string? email)
        {
            ApplicationUser user;
            if (User.IsInRole("User") || User.IsInRole("Admin") && email == null) { user = await _userManager.GetUserAsync(User); }
            else user = await _userManager.FindByEmailAsync(email);

            if (user == null) return NotFound(new Response("User not found", false));
            var userRole = await _userManager.GetRolesAsync(user);
            var userDetails = new VMUserDetails()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                RoleId = userRole[0],
            };
            return Ok(userDetails);
        }

        [HttpGet("RoleHelper")]
        public IActionResult GetRolesHelper()
        {
            var roles = _roleManager.Roles.Select(r => new
            {
                Label = r.Name,
                Value = r.Id,
            }).ToList();

            return Ok(roles);
        }


        [HttpPut("edit")]
        public async
            Task<IActionResult> EditUserDetails(VMUpdateUser newModel)
        {
            ApplicationUser user;
            if (User.IsInRole("User") || User.IsInRole("Admin") && newModel.Email == null) { user = await _userManager.GetUserAsync(User); }
            else user = await _userManager.FindByEmailAsync(newModel.Email);

            if (user == null) return NotFound(new Response("User not found", false));
            try
            {
                // var user = await _userManager.GetUserAsync(User);
                if (newModel.FirstName != null) user.FirstName = newModel.FirstName;
                if (newModel.LastName != null) user.LastName = newModel.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (newModel.RoleId != null && newModel.RoleId !="")
                    {
                        var spParams = new UpdateUserRoleInputModel
                        {
                            UserId = user.Id,
                            RoleId = newModel.RoleId,
                        };
                        _procedureManager.ExecStoreProcedure(StoredProcedure.UpdateUserRoles, spParams);
                    }
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
                var user = await _userManager.GetUserAsync(User);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<VMGetUserList>>> GetAllUsersAsync([FromQuery] GetUsersInputModel getUsersInputModel)
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
                UserIds = getUsersInputModel.UserIds ?? "",

            };

            var users = _procedureManager.ExecStoreProcedureMulResults<StoredProcedureCommonModel, VmSPGetUsers>(StoredProcedure.GetUsers, spParameters);

            var countData = users.Item1[0].Count;
            var employeesData = users.Item2;


            VMGetUserList getEmployees = new VMGetUserList
            {
                Count = (int)countData,
                Users = employeesData
            };

            var mappedEmployees = _mapper.Map<VMGetUserList>(getEmployees);
            if (mappedEmployees == null)
            {
                return NotFound(new Response("Data not found.", false));
            }
            return Ok(new Response<VMGetUserList>(mappedEmployees, true, "Data loaded successfully!"));

        }
        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateRole([FromBody] VMUpdateRole model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);


            if (user == null || role == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong", false));
            }

            var spParameters = new UpdateUserRoleInputModel
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
            var result = _procedureManager.ExecStoreProcedure(StoredProcedure.UpdateUserRoles, spParameters);
            if (result) return Ok(new Response("User updated successfully!", true));
            else return StatusCode(StatusCodes.Status500InternalServerError, new Response("Something went wrong, please try again.", false));
        }


        [HttpPost("add-user")]
        public async Task<IActionResult> Register([FromBody] VMAddUser model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null) return StatusCode(StatusCodes.Status500InternalServerError, new Response("User with same email already exists.", false));


            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false,
                CreatedDate = DateTime.Now,
            };

            var result = await _userManager.CreateAsync(user);

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
                if (model.RoleId == null)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
                else
                {
                    var role = await _roleManager.FindByIdAsync(model.RoleId);
                    var spParams = new UpdateUserRoleInputModel
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                    };
                    _procedureManager.ExecStoreProcedure(StoredProcedure.UpdateUserRoles, spParams);
                }
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var pwdToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmAddUserEmail", "Auth", new { userId = user.Id, token, pwdToken }, Request.Scheme);

                // Constructing the email template
                var emailTemplate = EmailTemplate.UserConfirmationMail(user.UserName, confirmationLink);

                MailRequest mailRequest = new MailRequest()
                {
                    RecipientEmail = model.Email,
                    Subject = "Confirmation Mail",
                    Body = emailTemplate,
                };

                await _emailService.SendEmailAsync(mailRequest);
                return Ok(new Response("User added successfully!"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response("Failed to register user. Please try again later.", false));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("toggle-user-state/{UserId}")]
        public async Task<IActionResult> ToggleState(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return BadRequest(new Response("User does not exist.", false));
            }

            user.IsActivated = !user.IsActivated;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(new Response("User updated successfully!", true));
            }
            else
            {
                var message = String.Empty;
                foreach (var error in result.Errors)
                {
                    message += error.Description;
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response(message, false));
            }

        }

        [HttpGet("UsersList")]
        public async Task<ActionResult<List<HelperModel>>> GetUsersList()
        {
            var users = _procedureManager.ExecStoreProcedure<HelperModel>(StoredProcedure.GetUsersWithNames);

            return Ok(new Response<List<HelperModel>>(users, true, "Data loaded successfully!"));
        }

    }
}