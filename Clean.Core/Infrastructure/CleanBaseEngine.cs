using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Clean.Core.Infrastructure
{
    public class CleanBaseEngine : IEngine
    {
        private ContainerManager _containerManager;

        public void Initialize()
        {
            RegisterDependenies();
        }

        private void RegisterDependenies()
        {
            var builder = new ContainerBuilder();

            var typeFinder = new WebAppTypeFinder();

            var clTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var clInstances = new List<IDependencyRegistrar>();

            foreach (var item in clTypes)
                clInstances.Add((IDependencyRegistrar)Activator.CreateInstance(item));

            foreach (var item in clInstances)
                item.Register(builder);

            var container = builder.Build();

            this._containerManager = new ContainerManager(container);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }


        public ContainerManager ContainerManager => this._containerManager;
    }
}
