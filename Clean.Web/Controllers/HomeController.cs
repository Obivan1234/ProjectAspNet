using Clean.Core.Domain.Temp;
using Clean.Data;
using Clean.Services.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clean.Web.Localization;
using Clean.Services.Localization;
using Clean.Core.Domain.Localization;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class HomeController : Controller
    {
        #region prop

        private readonly ITempDomainService _tempDomainService;
        private readonly ILanguageService _languageService;
        private readonly ILocalStringResourceService _lsrService;

        #endregion

        #region ctor

        public HomeController(ITempDomainService tempDomainService,
            ILanguageService languageService,
            ILocalStringResourceService lsrService)
        {
            this._tempDomainService = tempDomainService;
            this._languageService = languageService;
            this._lsrService = lsrService;
        }

        #endregion

        public ActionResult Index()
        {
            var lang = this._languageService.GetAllLanguages();
            

            return View();
        }
        
    }
}