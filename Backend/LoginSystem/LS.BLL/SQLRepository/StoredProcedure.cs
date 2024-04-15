using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BLL.SQLRepository
{
    public static class StoredProcedure
    {
        public const string GetUsers = "[usp_GetUsers]";
        public const string GetLoginHistory = "[usp_GetLoginHistory]";
    }
}
