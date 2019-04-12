using System;
using System.Collections.Generic;
using System.Text;

namespace VchyORMAttribute
{
    public class FieldAttribute : Attribute
    {
        public FieldAttribute(string field)
        {
            FieldName = field;
        }
        public string FieldName { get; set; }

        public bool IsKey { get; set; }

        public bool IsAllowedNull { get; set; }
    }
}
