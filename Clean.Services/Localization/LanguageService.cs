using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Data;
using Clean.Core.Domain.Localization;

namespace Clean.Services.Localization
{
    public class LanguageService : ILanguageService
    {
        private readonly IRepository<Language> _languageRepository;

        public LanguageService(IRepository<Language> languageRepository)
        {
            this._languageRepository = languageRepository;
        }



        public IEnumerable<Language> GetAllLanguages(bool alsoShowUnPublished = false)
        {
            var languages = this._languageRepository.Get();

            if (alsoShowUnPublished == false)
                languages.Where(w => w.Published == true);

            return languages;
        }

        public Language GetLanguageById(int id)
        {
            return this._languageRepository.GetById(id);
        }
    }
}
