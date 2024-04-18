using Alms.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alms.DAL.ViewModels
{
    public class StoreProcedureInputModel
    {

        public string Field { get; set; } = "";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Sort { get; set; } = "asc";
    }

    public class GetUsersInputModel : StoreProcedureInputModel
    {
        public string? Text { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? UserIds { get; set; }

    }


    public class GetLoginHistoryInputModel : StoreProcedureInputModel
    {
        public string? Text { get; set; }
        public string? UserIds { get; set; }
        public string? FromDate { get; set; }

        public string? ToDate { get; set; }

    }

    public class UpdateUserRoleInputModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
    }

}