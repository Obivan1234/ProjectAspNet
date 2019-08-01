using Clean.Core.Domain.ProductItem;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Data.Mapping.ProductItem
{
    public class PictureMap: EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            this.ToTable("Picture");
            this.HasKey(k => k.Id);
            this.Property(p => p.PictureBinary).IsRequired();
            this.Property(p => p.Description).IsRequired();
            this.Property(p => p.MimeType).IsRequired().HasMaxLength(50);
        }
    }
}
