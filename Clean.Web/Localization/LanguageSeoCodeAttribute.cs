using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clean.Web.Localization
{
    public class LanguageSeoCodeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null || filterContext.HttpContext == null)
                return;

            var request = filterContext.HttpContext.Request;

            if (request == null)
                return;

            if (filterContext.IsChildAction)
                return;

            if (!String.Equals(request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            if (filterContext.RouteData == null || filterContext.RouteData.Route == null)
                return;

            var pageUrl = request.RawUrl;
            var applicationPath = request.ApplicationPath;

            //var workContext = EngineContext.Current.Resolve<IWebWorkContext>();


            if (pageUrl.IsLocalizedUrl(applicationPath, true))
            {
                var code = pageUrl.GetLanguageSeoCodeFromUrl(applicationPath, true);

                if (code == "ua")
                    return;
                else
                    pageUrl = pageUrl.RemoveLanguageSeoCodeFromUrl(applicationPath, true);
            }

            pageUrl = pageUrl.AddLanguageSeoCodeToUrl("ua");
            filterContext.Result = new RedirectResult(pageUrl, false);
        }
    }
}