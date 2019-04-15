using Dapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VchyModel;
using VchyORMCommon.Enum;
using VchyORMDbBase;
using VchyORMFactory.Factotry;
using VchyORMFactory.Interface;
using VchyORMSqlserver;

namespace VchyORMFactory
{
    public class DbCRUD : BaseDapper, IDbInsert, IDbDelete, IDbUpdate
    {
        private BaseSql _sql;
        public DbCRUD() : base()
        {
            InstallSql();
        }
        public DbCRUD(string name) : base(name)
        {
            InstallSql();
        }
        public DbCRUD(string name, DbSource source) : base(name, source)
        {
            InstallSql();
        }

        #region Insert

        public int ExcuteInsert(BaseEntity model)
        {
            var sql = _sql.CreateInsert(model);
            return Connection.Execute(sql.ToString());
        }

        public T ExcuteInsertAndResultKey<T>(BaseEntity model)
            where T : class
        {
            var sql = _sql.CreateInsertAndResultKey(model);
            return Connection.QueryFirst<T>(sql.ToString());
        }

        public int ExcuteInsert<T>(List<T> models)
            where T : BaseEntity, new()
        {
            var sql = _sql.CreateInsert(models);
            return Connection.Execute(sql.ToString());
        }

        #endregion

        #region Delete

        public int ExcuteDelete(BaseEntity model)
        {
            var sql = _sql.CreateDelete(model);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteDelete(List<BaseEntity> models)
        {
            var sql = _sql.CreateDelete(models);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteDelete<T>(int key)
           where T : BaseEntity
        {
            var sql = _sql.CreateDelete<T>(key);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteDelete<T>(string key)
            where T : BaseEntity
        {
            var sql = _sql.CreateDelete<T>(key);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteDelete<T>(List<T> models)
            where T : BaseEntity, new()
        {
            var sql = _sql.CreateDelete(models);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteDelete<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            var sql = _sql.CreateDelete(expression);
            return Connection.Execute(sql.ToString());
        }
        #endregion

        #region Update

        public int ExcuteUpdate(BaseEntity model)
            => Connection.Execute(_sql.CreateUpdate(model).ToString());

        public int ExcuteUpdateOnUpdateFields(BaseEntity model, params string[] fields)
        => Connection.Execute(_sql.CreateUpdateOnNotUpdateFields(model, fields).ToString());

        public int ExcuteUpdateOnNotUpdateFields(BaseEntity model, params string[] fields)
        => Connection.Execute(_sql.CreateUpdateOnNotUpdateFields(model, fields).ToString());

        public int ExcuteUpdate<T>(T model, Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        => Connection.Execute(_sql.CreateUpdate(model, expression).ToString());

        public int ExcuteUpdateOnUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new()
        => Connection.Execute(_sql.CreateUpdateOnUpdateFields(model, expression, fields).ToString());

        public int ExcuteUpdateOnNotUpdateFields<T>(T model, Expression<Func<T, bool>> expression, params string[] fields)
            where T : BaseEntity, new()
        => Connection.Execute(_sql.CreateUpdateOnNotUpdateFields(model, expression, fields).ToString());

        #endregion

        #region private method

        private void InstallSql()
        {
            switch (Source)
            {
                case DbSource.SqlServer:
                case DbSource.Mysql:
                case DbSource.Sqlite:
                    _sql = new DbSqlServerCRUD();
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
