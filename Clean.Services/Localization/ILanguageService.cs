using Clean.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Services.Localization
{
    public interface ILanguageService
    {

        IEnumerable<Language> GetAllLanguages(bool showUnPublished = false);

        Language GetLanguageById(int id);

    }
}
