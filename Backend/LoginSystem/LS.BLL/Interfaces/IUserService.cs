using LS.DAL.ViewModels;

namespace LS.BLL.Interfaces
{
    public interface IUserService
    {
        Task<Response<VMUserDetails>> GetUserDetails(string email);
        Task<Response> EditUserDetails(string email, VMUpdateUser newModel);
        Task<Response> ChangePassword(VMChangePassword model);
    }
}
