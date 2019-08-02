using Clean.Core.Domain.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data.Mapping.ApplicationUser
{
    class LoginModelMap : EntityTypeConfiguration<LoginModel>
    {
        public LoginModelMap()
        {
            this.ToTable("Login");
            this.HasKey(f => f.Id);
            this.Property(f => f.UserName);
            this.Property(d => d.Password);
        }
    }
}
