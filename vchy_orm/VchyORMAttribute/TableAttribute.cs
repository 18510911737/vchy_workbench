using System;
using System.Collections.Generic;
using System.Text;

namespace VchyORMAttribute
{
    public class TableAttribute : Attribute
    {
        public TableAttribute(string table)
        {
            TableName = table;
        }
        public string TableName { get; set; }
    }
}
