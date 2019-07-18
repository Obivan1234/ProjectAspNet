using Clean.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data
{
    public class CleanBaseContext: DbContext, IDbContext
    {

        public CleanBaseContext(): base("CleanBase")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //search all mapping classes
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t=> !string.IsNullOrEmpty(t.Namespace))
                .Where(t=>t.BaseType != null  && t.BaseType.IsGenericType &&
                t.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var item in typesToRegister)
            {
                dynamic instance = Activator.CreateInstance(item);
                modelBuilder.Configurations.Add(instance);
            }


            base.OnModelCreating(modelBuilder);
        }

        ///https://stackoverflow.com/questions/1014295/new-keyword-in-method-signature
        ///https://www.c-sharpcorner.com/article/virtual-override-new-keywords-in-C-Sharp/
        ///бо в DbContext містить вже метод Set , але так як  використовується інтерфейс IDContext (бо принцип DIP) і тому  треба реалізувати і щоб конфлікту небуло
        public new IDbSet<T> Set<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

        public new DbEntityEntry<T> Entry<T>(T entity) where T : BaseEntity
        {
            return base.Entry<T>(entity);
        }
    }
}
