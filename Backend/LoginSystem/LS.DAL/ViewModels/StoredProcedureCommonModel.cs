using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alms.DAL.ViewModels
{
    public class StoredProcedureCommonModel
    {

        public int? Count { get; set; }
    }

    public class HelperModel
    {
        public string? Label { get; set; }
        public int? Value { get; set; }
    }
}
