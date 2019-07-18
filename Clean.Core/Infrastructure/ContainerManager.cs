using Autofac;
using Autofac.Core.Lifetime;
using System;
using System.Web;
using Autofac.Integration.Mvc;

namespace Clean.Core.Infrastructure
{
    public class ContainerManager
    {
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        public IContainer Container => _container;


        public T Resolve<T>(ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
                scope = GetLifeTimeScope();

            return scope.Resolve<T>();
        }


        public ILifetimeScope GetLifeTimeScope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;


                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

    }
}