﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Domain.ProductItem
{
    public class Picture: BaseEntity
    {
        public byte[] PictureBinary { get; set; }

        public string Description { get; set; }

        public string MimeType { get; set; }

        public string ApplicationUserMyId { get; set; }

        public Clean.Core.Domain.ApplicationUser.ApplicationUser ApplicationUser { get; set; }
    }
}
