using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VchyORMDbBase;
using VchyORMCommon;
using VchyORMSql.Interface;
using VchyORMAttribute;
using VchyModel;

namespace VchyORMSqlserver
{
    public class DbSqlServerCRUD : BaseSql
    {
        public override StringBuilder CreateInsert(BaseEntity model)
        {
            if (model.IsNull())
            {
                throw new ArgumentNullException("model", "param is null");
            }
            var r = new StringBuilder();
            var type = model.GetType();
            var properties = type.GetProperties();
            var insert = new StringBuilder($"insert into [{model.SelectTableName()}] (");
            var values = new StringBuilder(" values(");
            foreach (var info in properties)
            {
                var attr = info.GetCustomAttributes(typeof(FieldAttribute), false).FirstOrDefault() as FieldAttribute;
                if (attr == null)
                {
                    continue;
                }
                if (attr.IsKey)
                {
                    continue;
                }
                var val = info.GetValue(model, null);
                if (val == null && attr.IsAllowedNull)
                {
                    continue;
                }
                insert.Append($"[{attr.FieldName}],");
                values.Append($@"'{val ?? ""}',");
            }
            insert = new StringBuilder(insert.ToString().TrimEnd(',')).Append(")");
            values = new StringBuilder(values.ToString().TrimEnd(',')).Append(")");
            r.AppendFormat("{0} {1}", insert, values);
            return r;
        }

        public override StringBuilder CreateInsert(List<BaseEntity> model)
        {
            throw new NotImplementedException();
        }
    }
}
