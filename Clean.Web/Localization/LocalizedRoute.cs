using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Clean.Web.Localization
{
    public class LocalizedRoute : Route
    {
        public LocalizedRoute(string url, IRouteHandler routeHandler) 
            : base( url, routeHandler)
        { }

        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        { }

        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        { }

        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataToken, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataToken, routeHandler)
        { }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
            var appPath = httpContext.Request.ApplicationPath;

            if (virtualPath.IsLocalizedUrl(appPath, true))
            {
                var rawUrl = httpContext.Request.RawUrl;
                var newVirtualPath = rawUrl.RemoveLanguageSeoCodeFromUrl(appPath, true);

                if (string.IsNullOrEmpty(newVirtualPath))
                    newVirtualPath = "/";

                newVirtualPath = newVirtualPath.RemoveApplicationPathFromUrl(appPath);

                httpContext.RewritePath(newVirtualPath, true);
            }
            return base.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            var data = base.GetVirtualPath(requestContext, values);

            if(data != null)
            {
                string rawUrl = requestContext.HttpContext.Request.RawUrl;
                string applicationPath = requestContext.HttpContext.Request.ApplicationPath;

                if (rawUrl.IsLocalizedUrl(applicationPath, true))
                {
                    data.VirtualPath = string.Concat(rawUrl.GetLanguageSeoCodeFromUrl(applicationPath, true), "/",
                        data.VirtualPath);
                }
            }
            return data;
        }
    }
}