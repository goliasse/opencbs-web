using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace OpenCBS.Web.Model
{
    public class User : EntityBase, IUserIdentity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<Role> Roles { get; set; }
        public bool IsSuperuser { get; set; }

        public bool Can(string permission)
        {
            if (IsSuperuser) return true;
            return Roles.Select(r => r.Can(permission)).Aggregate((x, y) => x || y);
        }

        public bool HasServicePermission(string servicePermission)
        {
            if (IsSuperuser) return true;
            return Roles.Select(r => r.HasServicePermission(servicePermission)).Aggregate((x, y) => x || y);
        }

        public IEnumerable<string> Claims
        {
            get { return new List<string>(); }
        }
    }
}
