using System.Collections.Generic;
using System.Linq;
using Dapper;
using OpenCBS.Web.Interface.Repository;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Repository
{
    public class AlertRepository : Repository, IAlertRepository
    {
        public AlertRepository(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider) { }

        public IEnumerable<Alert> FindAll(int userId)
        {
            using (var connection = GetConnection())
            {
                const string sql = @"
                    select contract_code ContractCode, late_days LateDays
                    from dbo.Alerts(getdate(), @userId, 0)
                ";
                return connection.Query<Alert>(sql, new { userId }).ToList().AsReadOnly();
            }
        }
    }
}
