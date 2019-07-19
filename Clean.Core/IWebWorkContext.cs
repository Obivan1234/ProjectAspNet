using Clean.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core
{
    public interface IWebWorkContext
    {
        Language WoorkingLanguage { get; set; }
    }
}
