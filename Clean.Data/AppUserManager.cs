using Clean.Core.Domain.ApplicationUser;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        public AppUserManager(IUserStore<ApplicationUser> store)
           : base(store)
        {

        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
                                               IOwinContext context)
        {
            CleanBaseContext db = context.Get<CleanBaseContext>();
            AppUserManager manager = new AppUserManager(new UserStore<ApplicationUser>(db));

            return manager;
        }
    }
}
