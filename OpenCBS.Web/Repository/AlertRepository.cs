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
                    select
                        a.contract_code ContractCode,
                        a.late_days LateDays,
                        a.client_name ClientName,
                        a.amount Amount,
                        u.first_name + ' ' + u.last_name LoanOfficer,
                        a.branch_name BranchName
                    from dbo.Alerts(getdate(), @userId, 0) a
                    left join Users u on u.id = a.loan_officer_id
                    order by a.late_days desc
                ";
                return connection.Query<Alert>(sql, new { userId }).ToList().AsReadOnly();
            }
        }
    }
}
