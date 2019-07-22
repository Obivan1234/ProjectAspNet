using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clean.Web.Localization;
using System.Web.Routing;

namespace Clean.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapLocalizedRoute("HomePage",
                "",
                new { controller = "Home", action = "Index" },
                new[] { "Clean.Web.Controllers" }
                );

            routes.MapLocalizedRoute("ChangeLang",
                "changelanguage",
                new { controller = "Common", action = "SetLanguage" },
                new[] { "Clean.Web.Controllers" }
                );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
