using System;
using Nancy;
using OpenCBS.Web.Interface.Repository;

namespace OpenCBS.Web.Api
{
    public class UserModule : NancyModule // : SecureModule
    {
        public UserModule(IUserRepository userRepository)
        {
            Get["/api/users/current"] = x =>
            {
                return Context.CurrentUser;
            };

            Get["/api/users/{id:int}"] = _ =>
            {
                try
                {
                    System.Threading.Thread.Sleep(2000);
                    return userRepository.FindById((int) _.id);
                }
                catch (Exception error)
                {
                    System.Diagnostics.Debug.WriteLine(error.Message);
                    return null;
                }
            };
        }
    }
}
