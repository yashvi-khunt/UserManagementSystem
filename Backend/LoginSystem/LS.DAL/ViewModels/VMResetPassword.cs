using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DAL.ViewModels
{
    public class VMResetPassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token {  get; set; }
    }
}
