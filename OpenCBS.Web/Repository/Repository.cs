using System.Data;
using System.Data.SqlClient;
using OpenCBS.Web.Interface.Repository;

namespace OpenCBS.Web.Repository
{
    public abstract class Repository
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        protected Repository(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        protected IDbConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionStringProvider.GetConnectionString());
            connection.Open();
            return connection;
        }
    }
}
