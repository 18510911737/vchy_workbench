using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;
using VchyORMCommon.Enum;
using VchyORMDbBase;
using VchyORMFactory.Factotry;
using VchyORMFactory.Interface;
using VchyORMSqlserver;

namespace VchyORMFactory
{
    public class DbCRUD : BaseDapper, IDbInsert
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

        public int ExcuteInsert(BaseEntity model)
        {
            var sql = _sql.CreateInsert(model);
            return Connection.Execute(sql.ToString());
        }

        public int ExcuteInsert<T>(List<T> models)
            where T : BaseEntity,new()
        {
            var sql = _sql.CreateInsert(models);
            return Connection.Execute(sql.ToString());
        }
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
    }
}
