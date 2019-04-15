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
using System.Reflection;

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

        #endregion

        #region Update

        public override StringBuilder CreateUpdate(BaseEntity model)
        {
            if (model.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var type = model.GetType();
            var key = model.GetFieldByKey();
            var fields = model.GetFieldsByNotKey();
            var r = new StringBuilder();
            r.Append($"Update [{model.SelectTableName()}] ");
            r.Append("Set ");
            foreach (var field in fields)
            {
                r.Append($"{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
                r.Append("=");
                r.Append($"{type.GetProperty(field.Name).GetValue(model)}, ");
            }
            r = new StringBuilder(r.ToString().TrimEnd(','));
            r.Append($"WHERE {(key.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($"{type.GetProperty(key.Name).GetValue(model)}");
            return r;
        }

        public override StringBuilder CreateUpdate<T>(T model, Expression<Func<T, bool>> expression)
        {
            return base.CreateUpdate(model, expression);
        }

        public override StringBuilder CreateUpdateOnNotUpdateFields(BaseEntity model, params string[] fields)
        => CreateUpdate(model, false, fields);

        public override StringBuilder CreateUpdateOnNotUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
        {
            return base.CreateUpdateOnNotUpdateFields(model, expression, fields);
        }

        public override StringBuilder CreateUpdateOnUpdateFields(BaseEntity model, params string[] fields)
            => CreateUpdate(model, true, fields);

        public override StringBuilder CreateUpdateOnUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
        {
            return base.CreateUpdateOnUpdateFields(model, expression, fields);
        }

        private StringBuilder CreateUpdate(BaseEntity model, bool IsUpdateFields, params string[] fields)
        {
            if (model.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var type = model.GetType();
            var key = model.GetFieldByKey();
            PropertyInfo[] properties = null;
            if (IsUpdateFields)
            {
                properties = model.GetFieldsByNotKey().Where(f => fields.Any(w => w == f.Name)).ToArray();
            }
            else
            {
                properties = model.GetFieldsByNotKey().Where(f => !fields.Any(w => w == f.Name)).ToArray();
            }
            var r = new StringBuilder();
            r.Append($"Update [{model.SelectTableName()}] ");
            r.Append("Set ");
            foreach (var field in properties)
            {
                r.Append($"{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
                r.Append("=");
                r.Append($"{type.GetProperty(field.Name).GetValue(model)}, ");
            }
            r = new StringBuilder(r.ToString().TrimEnd(','));
            r.Append($"WHERE {(key.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($"{type.GetProperty(key.Name).GetValue(model)}");
            return r;
        }

        #endregion

        #region Select

        public override StringBuilder CreateSelect(BaseEntity model)
        {
            if (model.IsNull())
            {
                throw new ArgumentNullException("param is null");
            }
            var r = new StringBuilder();
            var type = model.GetType();
            var key = model.GetFieldByKey();
            r.Append($"SELECT * FOM {model.SelectTableName()} AS f ");
            r.Append($@"WHERE f.{(key.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($"'{type.GetProperty(key.Name).GetValue(model)}'");
            return r;
        }

        public override StringBuilder CreateSelect<T>(int key)
        => CreateSelect<T>(key.ToString());


        public override StringBuilder CreateSelect<T>(string key)
        {
            var r = new StringBuilder();
            var type = typeof(T);
            var field = type.GetFieldByKey();
            r.Append($"SELECT * FOM {type.SelectTableName()} AS f ");
            r.Append($@"WHERE f.{(field.GetCustomAttributes(typeof(FieldAttribute), false)[0] as FieldAttribute).FieldName}");
            r.Append("=");
            r.Append($"'{key}'");
            return r;
        }

        public override StringBuilder CreateSelect<T>()
        {
            var r = new StringBuilder();
            var type = typeof(T);
            var field = type.GetFieldByKey();
            r.Append($"SELECT * FOM {type.SelectTableName()} AS f ");
            return r;
        }

        public override StringBuilder CreateSelectFirst<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public override StringBuilder CreateSelectList<T>(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
