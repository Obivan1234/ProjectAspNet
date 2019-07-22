using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Clean.Core.Domain;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Clean.Data;

[assembly: OwinStartup(typeof(Clean.Web.App_Start.Startup))]

namespace Clean.Web.App_Start
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new CleanBaseContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}