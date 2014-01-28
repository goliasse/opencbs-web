using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using OpenCBS.Web.Interface;

namespace OpenCBS.Web
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider
        {
            get { return new RootPathProvider(); }
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);

            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("vendor", "vendor", new[] { "js" }));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("assets", "assets", new[] { "js", "css", "map", "svg" }));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts", "fonts", new[] { "otf", "eot", "ttf", "svg", "woff" }));
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            pipelines.BeforeRequest.AddItemToStartOfPipeline(GetAuthenticationHook(container));
        }

        private static Func<NancyContext, Response> GetAuthenticationHook(TinyIoCContainer container)
        {
            return context =>
            {
                var guid = GetGuid(context);
                if (guid == Guid.Empty) return null;

                var sessionCache = container.Resolve<ISessionCache>();
                context.CurrentUser = sessionCache.Get(guid);

                return null;
            };
        }

        private static Guid GetGuid(NancyContext context)
        {
            if (!context.Request.Cookies.ContainsKey("auth_token")) return Guid.Empty;

            var cookieValue = context.Request.Cookies["auth_token"];

            Guid guid;
            if (string.IsNullOrEmpty(cookieValue) || !Guid.TryParse(cookieValue, out guid)) return Guid.Empty;

            return guid;
        }
    }
}
