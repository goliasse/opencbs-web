using System.IO;
using Nancy;

namespace OpenCBS.Web
{
    public class ApplicationModule : NancyModule
    {
        public ApplicationModule(IRootPathProvider pathProvider)
        {
            Get["/"] =
            Get[@"/(.*)"] = x =>
            {
                var file = pathProvider.GetRootPath();
                file = Path.Combine(file, "index.html");
                return Response.AsText(File.ReadAllText(file), "text/html");
            };
        }
    }
}
