using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using VchyORMCommon.Enum;
using VchyORMDbBase;
using VchyORMSqlserver;

namespace VchyORMFactory.Factotry
{
    public abstract class BaseDapper
    {
        private string _conn;

        private IDbTransaction _transaction;

        public IDbConnection Connection { get; private set; }
        public DbSource Source { get; private set; }

        public BaseDapper() : this("dapper")
        {

        }
        public BaseDapper(string name) : this(name, DbSource.SqlServer)
        {

        }
        public BaseDapper(string name, DbSource source)
        {
            SetConnection(name);
            Source = source;
            SetDbConnection();
        }

        private void SetDbConnection()
        {
            switch (Source)
            {
                case DbSource.SqlServer:
                case DbSource.Mysql:
                case DbSource.Sqlite:
                    Connection = new SqlConnection(_conn);
                    break;
                default:
                    throw new NotImplementedException($"Invalid data connection by {Source}");
            }
        }

        public virtual void SetConnection(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException($"The param is null or white space", "name");
            }
            _conn = ConfigHelper.Configuration.GetConnectionString(name);
            if (string.IsNullOrWhiteSpace(_conn))
            {
                throw new ArgumentException($"Cannot get the database connection string by {name}", "name");
            }
        }

        public virtual IDbTransaction BeginTransaction()
        {
            _transaction = Connection.BeginTransaction();
            return _transaction;
        }

        public virtual void Commit()
        {
            _transaction.Commit();
        }

        public virtual void RollBack()
        {
            _transaction.Rollback();
        }

    }
}
