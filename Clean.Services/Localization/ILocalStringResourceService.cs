using Clean.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Services.Localization
{
    public interface ILocalStringResourceService
    {
        LocalStringResource GetLSRById(int id);

        IEnumerable<LocalStringResource> GetLSRByName(string resourceName);

        LocalStringResource GetLSRByNameAndLanguageId(string resourceName, int languageId);

        IEnumerable<LocalStringResource> GetLSRByLanguageId(int languageId);

        void InsertLSR(LocalStringResource localeStringResource);

        void UpdateLSR(LocalStringResource localeStringResource);

        void DeleteLSR(LocalStringResource localeStringResource);
    }
}
