using LS.BLL.Interfaces;
using LS.DAL.Models;
using LS.DAL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace LS.BLL.Services
{
    public class UserService: IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response> ChangePassword(VMChangePassword model)
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
                        return new Response("Password Changed successfully.", true);
                    }
                    else
                    {
                        return new Response("Something went wrong.", false);
                    }

                }
                return new Response("Something went wrong.", false);
            }
            catch (Exception ex)
            {
                return new Response("Something went wrong.", false);
            }
        }

        public async Task<Response> EditUserDetails(string email, VMUpdateUser newModel)
        {

            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (newModel.FirstName != null) user.FirstName = newModel.FirstName;
                if (newModel.LastName != null) user.LastName = newModel.LastName;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new Response("User updated successfully", true);
                }
                else
                {
                    return new Response("Something went wrong.", false);
                }
            }
            catch (Exception ex)
            {
                return new Response("Something went wrong.", false);
            }
        }

        public async Task<Response<VMUserDetails>> GetUserDetails(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return new Response<VMUserDetails>("User does not exist.", false);
            }

            var userDetails = new VMUserDetails()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return new Response<VMUserDetails>(userDetails);
        }
    }
}
