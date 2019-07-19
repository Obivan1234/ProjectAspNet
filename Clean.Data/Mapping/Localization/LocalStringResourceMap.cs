using Clean.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data.Mapping.Localization
{
    public class LocalStringResourceMap: EntityTypeConfiguration<LocalStringResource>
    {
        public LocalStringResourceMap()
        {
            this.ToTable("LocalStringResource");
            this.HasKey(k => k.Id);
            this.Property(p => p.LanguageId).IsRequired();
            this.Property(p => p.ResourceName).IsRequired().HasMaxLength(250);
            this.Property(p => p.ResourceValue).IsRequired();

            this.HasRequired(r => r.Language)
                .WithMany()
                .HasForeignKey(k => k.LanguageId);
        }
    }
}
