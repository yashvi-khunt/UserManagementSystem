using LS.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BLL.Repositories
{
    public interface IAuthService
    {
        Task<object> Login(LoginModel model);
        Task<object> Register(RegisterModel model);
        Task<object> ConfirmEmail(string userId, string token);
    }
}
