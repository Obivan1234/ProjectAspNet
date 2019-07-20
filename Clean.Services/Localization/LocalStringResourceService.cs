using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Core.Caching;
using Clean.Core.Data;
using Clean.Core.Domain.Localization;

namespace Clean.Services.Localization
{
    public class LocalStringResourceService : ILocalStringResourceService
    {
        private const string LOCALSTRINGRESOURCES_ALL_KEY = "Clean.lsr.all-";

        #region prop

        private readonly IRepository<LocalStringResource> _lsrRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region ctor

        public LocalStringResourceService(IRepository<LocalStringResource> lsrRepository,
            ICacheManager cacheManager)
        {
            this._lsrRepository = lsrRepository;
            this._cacheManager = cacheManager;
        }

        #endregion



        public void DeleteLSR(LocalStringResource localeStringResource)
        {
            this._lsrRepository.Delete(localeStringResource);

            string key = LOCALSTRINGRESOURCES_ALL_KEY + localeStringResource.LanguageId;

            this._cacheManager.Remove(key);

            var resources = this._lsrRepository.Get(w => w.LanguageId == localeStringResource.LanguageId);

            this._cacheManager.Set(key, resources, 60);
        }

        public LocalStringResource GetLSRById(int id)
        {
            return this._lsrRepository.GetById(id);
        }

        public IEnumerable<LocalStringResource> GetLSRByName(string resourceName)
        {
            return this._lsrRepository.Get(w => w.ResourceName == resourceName);
        }

        public LocalStringResource GetLSRByNameAndLanguageId(string resourceName, int languageId)
        {
            string key = LOCALSTRINGRESOURCES_ALL_KEY + languageId;

            var resources = this._cacheManager.Get(key, () =>
            {
                return this._lsrRepository.Get(w => w.LanguageId == languageId);
            });

            return resources.Where(w => w.ResourceName == resourceName).FirstOrDefault();

        }

        public IEnumerable<LocalStringResource> GetLSRByLanguageId(int languageId)
        {
            string key = LOCALSTRINGRESOURCES_ALL_KEY + languageId;

            var resources = this._cacheManager.Get(key, () =>
            {
                return this._lsrRepository.Get(w => w.LanguageId == languageId);
            });

            return resources;
        }


        public void InsertLSR(LocalStringResource localeStringResource)
        {
            string key = LOCALSTRINGRESOURCES_ALL_KEY + localeStringResource.LanguageId;

            this._lsrRepository.Insert(localeStringResource);

            this._cacheManager.Remove(key);

            var resources = this._lsrRepository.Get(w => w.LanguageId == localeStringResource.LanguageId);

            this._cacheManager.Set(key, resources, 60);

        }

        public void UpdateLSR(LocalStringResource localeStringResource)
        {
            string key = LOCALSTRINGRESOURCES_ALL_KEY + localeStringResource.LanguageId;

            this._lsrRepository.Update(localeStringResource);

            this._cacheManager.Remove(key);

            var resources = this._lsrRepository.Get(w => w.LanguageId == localeStringResource.LanguageId);

            this._cacheManager.Set(key, resources, 60);
            
        }
    }
}
