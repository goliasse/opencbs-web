using System.Collections.Generic;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Interface.Repository
{
    public interface IAlertRepository
    {
        IEnumerable<Alert> FindAll(int userId);
    }
}
