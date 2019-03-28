using System.Data;
using System.Data.SqlClient;


namespace vchy_orm
{
    public class DapperFactory
    {
        private string _conn;

        public DapperFactory()
        {
            _conn = ConfigHelper.GetSectionValue("conn");
        }
        public DapperFactory(string conn)
        {
            _conn = conn;
        }

        public IDbConnection Connection => new SqlConnection(_conn);

    }
}
