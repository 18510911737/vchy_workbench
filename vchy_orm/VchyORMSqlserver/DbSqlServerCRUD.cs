using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VchyORMDbBase;
using VchyORMCommon;
using VchyORMSql.Interface;
using VchyORMAttribute;
using VchyModel;
using System.Linq.Expressions;

namespace VchyORMSqlserver
{
    public class DbSqlServerCRUD : BaseSql
    {
        #region Insert

        public override StringBuilder CreateInsert(BaseEntity model)
            => CreateInsert(model);

        public override StringBuilder CreateInsertAndResultKey(BaseEntity model)
            => CreateInsert(model, "SELECT SCOPE_IDENTITY()");

        private StringBuilder CreateInsert(BaseEntity model, string append = null)
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
            if (append.IsNotNull())
            {
                r.Append(append);
            }
            return r;
        }

        public override StringBuilder CreateInsert<T>(List<T> models)
        {
            if (models.IsNull())
            {
                throw new ArgumentNullException("models", "param is null");
            }
            var model = models.FirstOrDefault();
            if (model.IsNull())
            {
                throw new ArgumentException("models", "param is empty");
            }
            var r = new StringBuilder();
            var type = model.GetType();
            var properties = type.GetProperties();
            var insert = new StringBuilder($"insert into [{model.SelectTableName()}] (");
            var values = new StringBuilder("values");
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
            }
            foreach (var m in models)
            {
                values.Append("(");
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
                    var val = info.GetValue(m, null);
                    if (val == null && attr.IsAllowedNull)
                    {
                        continue;
                    }
                    values.Append($@"'{val ?? ""}',");
                }
                values = new StringBuilder(values.ToString().TrimEnd(',')).Append(")");
                values.Append(",");
            }
            insert = new StringBuilder(insert.ToString().TrimEnd(',')).Append(")");
            values = new StringBuilder(values.ToString().TrimEnd(','));
            r.AppendFormat("{0} {1}", insert, values);
            return r;
        }

        #endregion

        #region Delete

        public override StringBuilder CreateDelete(BaseEntity model)
        {
            if (model.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var type = model.GetType();
            var field = model.GetFieldByKey();
            var r = new StringBuilder();
            r.Append($@"DELETE FORM {model.SelectTableName()} WHERE");
            r.Append($@"{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($@"'{type.GetProperty(field.Name).GetValue(model, null) ?? ""}'");
            return r;
        }

        public override StringBuilder CreateDelete<T>(int key)
            => CreateDelete<T>(key.ToString());

        public override StringBuilder CreateDelete<T>(string key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var type = typeof(T);
            var field = type.GetFieldByKey();
            var r = new StringBuilder();
            r.Append($@"DELETE FORM {type.SelectTableName()} WHERE");
            r.Append($@"{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($@"'{key}'");
            return r;
        }

        public override StringBuilder CreateDelete<T>(List<T> models)
        {
            if (models.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var model = models.FirstOrDefault();
            if (model.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var type = model.GetType(); ;
            var field = type.GetFieldByKey();
            var r = new StringBuilder();
            r.Append($@"DELETE FORM {type.SelectTableName()} WHERE");
            r.Append($@"{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("in(");
            models.ForEach(f =>
            {
                r.Append($"'{type.GetProperty(field.Name).GetValue(f, null) ?? ""}',");
            });
            r = new StringBuilder(r.ToString().TrimEnd(',')).Append(")");
            return r;
        }

        public override StringBuilder CreateDelete<T>(Expression<Func<T, bool>> expression)
        {
            return base.CreateDelete(expression);
        }

        public override StringBuilder CreateDelete<T>(T type, Expression<Func<T, bool>> expression)
        {
            return base.CreateDelete(type, expression);
        }

        #endregion
    }
}
