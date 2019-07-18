using Clean.Core.Domain.Temp;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data.Mapping.Temp
{
    public class TempDomainMap: EntityTypeConfiguration<TempDomain>
    {
        public TempDomainMap()
        {
            this.ToTable("TempDomain");
            this.HasKey(k => k.Id);
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.Age).IsRequired();
        }
    }
}
