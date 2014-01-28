using System;
using Nancy;
using OpenCBS.Web.Interface;
using OpenCBS.Web.Interface.Repository;

namespace OpenCBS.Web.Api
{
    public class SessionModule : NancyModule
    {
        public SessionModule(IUserRepository userRepository, ISessionCache sessionCache)
        {
            Get["/api/sessions/{value}"] = x =>
            {
                System.Threading.Thread.Sleep(2000);
                return new
                {
                    id = "12345",
                    user = new
                    {
                        id = 1,
                        firstName = "Pavel",
                        lastName = "Bastov"
                    }
                };
            };

            Post["/api/sessions"] = x =>
            {
                var username = (string) Request.Form.Username;
                var password = (string) Request.Form.Password;

                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password)) return HttpStatusCode.Unauthorized;

                var user = userRepository.FindByUsernameAndPassword(username, password);
                if (user == null) return HttpStatusCode.Unauthorized;

                var guid = Guid.NewGuid();
                sessionCache.Set(guid, user);
                return new { authToken = guid };
            };

            Delete["/api/sessions/{value:guid}"] = x =>
            {
                sessionCache.Remove(x.value);
                return HttpStatusCode.OK;
            };
        }
    }
}
