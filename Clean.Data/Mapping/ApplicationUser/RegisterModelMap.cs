using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Domain.ApplicationUser;

namespace Clean.Data.Mapping.ApplicationUser
{
    public class RegisterModelMap : EntityTypeConfiguration<RegisterModel>
    {
        public RegisterModelMap()
        {
            this.ToTable("RegisterModel", "init");
            this.HasKey(k => k.Id);
            this.Property(p => p.UserName).IsRequired();
            this.Property(p => p.Description);
            this.Property(p => p.imageData);
            this.Property(p => p.Password).IsRequired();
            this.Property(p => p.PasswordConfirm).IsRequired();
        }
    }
}
