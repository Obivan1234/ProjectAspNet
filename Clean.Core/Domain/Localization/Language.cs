using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Domain.Localization
{
    public class Language: BaseEntity
    {
        public string Name { get; set; }

        public string LanguageCulture { get; set; }

        public string UniqueSeoCode { get; set; }

        public bool Published { get; set; }

        public int? Default { get; set; }
    }
}
