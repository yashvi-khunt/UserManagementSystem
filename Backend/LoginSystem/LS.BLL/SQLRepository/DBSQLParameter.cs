﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS.BLL.SQLRepository
{
    public class DBSQLParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public bool IsOutParam { get; set; } = false;



        public DBSQLParameter(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public DBSQLParameter(string name, object value, bool isOutParam)
        {
            Name = name;
            Value = value;
            IsOutParam = isOutParam;
        }

    }
}
