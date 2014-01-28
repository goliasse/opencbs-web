using System.Web.Configuration;
using OpenCBS.Web.Interface.Repository;

namespace OpenCBS.Web.Repository
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return WebConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
        }
    }
}
