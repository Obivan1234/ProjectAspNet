using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Data;
using Clean.Core.Domain.Temp;

namespace Clean.Services.Temp
{
    public class TempDomainService : ITempDomainService
    {

        private readonly IRepository<TempDomain> _tempRepository;

        public TempDomainService(IRepository<TempDomain> tempRepository)
        {
            this._tempRepository = tempRepository;
        }



        public void InsertData(TempDomain tempDomain)
        {
            this._tempRepository.Insert(tempDomain);
        }
    }
}
