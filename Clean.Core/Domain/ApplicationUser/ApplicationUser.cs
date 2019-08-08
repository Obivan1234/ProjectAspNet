using Clean.Core.Domain.ProductItem;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Domain.ApplicationUser
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; }

        public byte[] imageData { get; set; }

        public ICollection<Picture> photos { get; set; }
    }
}
