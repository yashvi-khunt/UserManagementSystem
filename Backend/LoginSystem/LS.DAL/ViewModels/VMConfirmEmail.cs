using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DAL.ViewModels
{
public class VMConfirmEmail
    {
        public string UserId {  get; set; }
        public string Token { get; set; }
    }
}

public class VMAddConfirmEmail 
{
    public string UserId { get; set; }
    public string Token { get; set; }
    public string PwdToken {  get; set; }

}
