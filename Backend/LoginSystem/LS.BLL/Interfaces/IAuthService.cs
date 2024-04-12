using LS.DAL.Models;
using LS.DAL.ViewModels;

namespace LS.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<Response<string>> Login(VMLogin model);
        Task<Response<ApplicationUser>> Register(VMRegister model);
        Task<Response> ConfirmEmail(VMConfirmEmail model);
        Task<Response<ApplicationUser>> ForgotPassword(VMForgotPassword model);
        Task<Response> ResetPassword(VMResetPassword model);
        
    }
}