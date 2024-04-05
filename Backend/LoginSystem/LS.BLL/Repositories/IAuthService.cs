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
        Task<object> Login(VMLogin model);
        Task<object> Register(VMRegister model);
        Task<Response> ConfirmEmail(VMConfirmEmail model);

        //Task<object> ConfirmPassword();
    }
}
