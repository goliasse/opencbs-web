using System;
using System.IO;
using Nancy;

namespace OpenCBS.Web
{
    public class RootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
#if DEBUG
            var path = Path.Combine(new[] { AppDomain.CurrentDomain.BaseDirectory, "..", "debug" });
            return Path.GetFullPath(path);
#else
            return string.Empty;
#endif
        }
    }
}
