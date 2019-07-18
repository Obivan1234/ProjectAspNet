using Autofac;
using Autofac.Integration.Mvc;
using Clean.Core.Data;
using Clean.Core.Infrastructure;
using Clean.Data;
using Clean.Services.Temp;

namespace Clean.Web.Infrastructure
{
    public class DependecyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<CleanBaseContext>().As<IDbContext>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();


            builder.RegisterType<TempDomainService>().As<ITempDomainService>().InstancePerLifetimeScope();
        }
    }
}