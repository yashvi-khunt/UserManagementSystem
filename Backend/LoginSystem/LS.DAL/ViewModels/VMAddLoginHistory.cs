using LS.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.DAL.ViewModels
{
    public class VMAddLoginHistory
    {

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }
        public string IpAddress { get; set; }

        public string? Browser { get; set; }

        public string? OS { get; set; }

        public string? Device { get; set; }
    }

    public class VMAddLoginHistoryResponse
    {
        public bool? IsValid { get; set; }

        public string? Message { get; set; }
    }
}
