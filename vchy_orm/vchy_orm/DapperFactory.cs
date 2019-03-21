using System.Data;
using System.Data.SqlClient;


namespace vchy_orm
{
    public class DapperFactory
    {
        private string _conn;

        public DapperFactory()
        {
            
        }

        public IDbConnection Connection => new SqlConnection();

    }
}
