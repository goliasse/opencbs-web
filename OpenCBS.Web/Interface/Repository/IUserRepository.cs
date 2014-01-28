using OpenCBS.Web.Model;

namespace OpenCBS.Web.Interface.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User FindByUsernameAndPassword(string username, string password);
//        bool UserExists(int id, string password);
//        void ChangePassword(int id, string password);
    }
}
