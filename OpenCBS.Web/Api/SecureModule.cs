using Nancy;
using Nancy.Security;

namespace OpenCBS.Web.Api
{
    public class SecureModule : NancyModule
    {
        public SecureModule()
        {
            this.RequiresAuthentication();
        }
    }
}