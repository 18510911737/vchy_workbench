using System;
using System.Collections.Generic;
using System.Text;
using VchyORMAttribute;
using VchyORMCommon;

namespace VchyModel
{
    public class BaseEntity
    {

    }
    public static class BaseEntityExtend
    {
        public static string SelectTableName<T>(this T entity)
              where T : BaseEntity, new()
        {
            var attr =entity.GetType().GetCustomAttributes(typeof(TableAttribute), false);
            if (attr.IsEmpty())
            {
                throw new MissingMemberException("not exist typeof(TableAttribute)");
            }
            return (attr[0] as TableAttribute).TableName;
        }
    }
}
