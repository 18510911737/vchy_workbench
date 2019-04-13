using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (entity.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            return entity.GetType().SelectTableName();
        }

        public static string SelectTableName(this Type type)
        {
            var attr = type.GetCustomAttributes(typeof(TableAttribute), false);
            if (attr.IsEmpty())
            {
                throw new MissingMemberException("not exist typeof(TableAttribute)");
            }
            return (attr[0] as TableAttribute).TableName;
        }

        public static PropertyInfo GetFieldByKey<T>(this T entity)
            where T : BaseEntity, new()
        {
            if (entity.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            return entity.GetType().GetFieldByKey();
        }

        public static PropertyInfo GetFieldByKey(this Type type)
        {
            var field = type.GetProperties().FirstOrDefault(f =>
            {
                var attr = f.GetCustomAttributes(typeof(FieldAttribute), false);
                if (attr != null)
                {
                    return (attr[0] as FieldAttribute).IsKey;
                }
                return false;
            });
            if (field.IsNull())
            {
                throw new ArgumentException("key is null by typeof(FieldAttribute)");
            }
            return field;
        }

        public static PropertyInfo[] GetFieldsByNotKey<T>(this T entity)
            where T : BaseEntity, new()
        {
            if (entity.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            return entity.GetType().GetFieldsByNotKey();
        }

        public static PropertyInfo[] GetFieldsByNotKey(this Type type)
        {
            var fields = type.GetProperties().Where(f =>
            {
                var attr = f.GetCustomAttributes(typeof(FieldAttribute), false);
                if (attr != null)
                {
                    return !(attr[0] as FieldAttribute).IsKey;
                }
                return false;
            }).ToArray();
            if (fields.IsEmpty())
            {
                throw new ArgumentException("fields is empty by typeof(FieldAttribute)");
            }
            return fields;
        }
    }
}
