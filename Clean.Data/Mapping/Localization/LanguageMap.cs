using Clean.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data.Mapping.Localization
{
    public class LanguageMap: EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            this.ToTable("Language");
            this.HasKey(k => k.Id);
            this.Property(p => p.Name).IsRequired();
            this.Property(p => p.LanguageCulture).IsRequired();
            this.Property(p => p.Published).IsRequired();
            this.Property(p => p.UniqueSeoCode).IsRequired();
            this.Property(p => p.Default).IsOptional();
        }
    }
}
