using Autofac;
using Autofac.Integration.Mvc;
using Clean.Core;
using Clean.Core.Caching;
using Clean.Core.Data;
using Clean.Core.Infrastructure;
using Clean.Data;
using Clean.Services.ApplicationUser;
using Clean.Services.Localization;
using Clean.Services.ProductItem;
using Clean.Services.Temp;
using System.Runtime.Caching.Hosting;

namespace Clean.Web.Infrastructure
{
    public class DependecyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CleanBaseContext>().As<IDbContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<WebWorkContext>().As<IWebWorkContext>().InstancePerLifetimeScope();
            builder.RegisterType<TempDomainService>().As<ITempDomainService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();
            builder.RegisterType<LocalStringResourceService>().As<ILocalStringResourceService>().InstancePerLifetimeScope();
            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();
            builder.RegisterType<LoginModelService>().As<ILoginModelService>().InstancePerLifetimeScope();
        }
    }
}