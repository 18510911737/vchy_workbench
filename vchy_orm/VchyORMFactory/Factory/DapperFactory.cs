using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VchyORMCommon.Enum;
using VchyORMDbBase;
using VchyORMFactory.Interface;

namespace VchyORMFactory.Factotry
{
    public class DapperFactory : DbCRUD
    {
        public DapperFactory() : base()
        {

        }
        public DapperFactory(string name) : base(name)
        {

        }
        public DapperFactory(string name, DbSource source) : base(name, source)
        {
        }
     
    }
}
