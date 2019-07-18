using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Domain.Temp
{
    public class TempDomain: BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
