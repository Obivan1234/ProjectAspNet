using Clean.Core.Domain.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Services.Temp
{
    public interface ITempDomainService
    {
        void InsertData(TempDomain tempDomain);
    }
}
