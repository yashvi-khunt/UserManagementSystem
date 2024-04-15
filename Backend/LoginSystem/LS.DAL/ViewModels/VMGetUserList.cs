using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DAL.ViewModels
{
    public class VmSPGetUsers
    {
        public long Id { get; set; }
        //public string? Id { get; set; }
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Role { get; set; }
        public bool isActivated { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

    }
    public class VMGetUserList
    {
        public int Count { get; set; }

        public List<VmSPGetUsers>? Users { get; set; }
    }
}
