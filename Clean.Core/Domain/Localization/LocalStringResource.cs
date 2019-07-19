using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Domain.Localization
{
    public class LocalStringResource: BaseEntity
    {
        public int LanguageId { get; set; }

        public string ResourceName { get; set; }

        public string ResourceValue { get; set; }

        public Language Language { get; set; }
    }
}
