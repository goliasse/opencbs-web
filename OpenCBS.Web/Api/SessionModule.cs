using System;
using Nancy;
using OpenCBS.Web.Interface;
using OpenCBS.Web.Interface.Repository;
using OpenCBS.Web.Model;

namespace OpenCBS.Web.Api
{
    public class SessionModule : NancyModule
    {
        public SessionModule(IUserRepository userRepository, ISessionCache sessionCache)
        {
            Get["/api/sessions/{value}"] = x =>
            {
                var guid = (Guid) x.value;
                var session = sessionCache.Get(guid);
                if (session == null)
                {
                    return new
                    {
                        isAuthenticated = false,
                    };
                }

                return new
                {
                    userId = session.User.Id,
                    firstName = session.User.FirstName,
                    lastName = session.User.LastName,
                    isAuthenticated = true
                };
            };

            Post["/api/sessions"] = x =>
            {
                var guid = (Guid) Request.Form.Id;
                var username = (string) Request.Form.Username;
                var password = (string) Request.Form.Password;

                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password)) return HttpStatusCode.Unauthorized;

                var user = userRepository.FindByUsernameAndPassword(username, password);
                if (user == null) return HttpStatusCode.Unauthorized;

                var session = new Session { Id = guid, User = user };
                sessionCache.Set(guid, session);

                return new
                {
                    id = guid,
                    isAuthenticated = true,
                    userID = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName
                };
            };

            Delete["/api/sessions/{value:guid}"] = x =>
            {
                sessionCache.Remove(x.value);
                return HttpStatusCode.NoContent;
            };
        }
    }
}
