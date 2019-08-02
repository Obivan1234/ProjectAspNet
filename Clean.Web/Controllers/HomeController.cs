﻿using Clean.Core.Domain.Temp;
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
using Clean.Web.Filter;
using Clean.Core.Domain.ApplicationUser;
using Clean.Core.Data;
using Clean.Services.ProductItem;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class HomeController : Controller
    {
        #region prop

        private readonly ITempDomainService _tempDomainService;
        private readonly ILanguageService _languageService;
        private readonly ILocalStringResourceService _lsrService;
        private readonly IRepository<Language> _langRepository;
        private readonly IPictureService _pictureService;

        #endregion

        #region ctor

        public HomeController(ITempDomainService tempDomainService,
            ILanguageService languageService,
            ILocalStringResourceService lsrService,
            IRepository<Language> langRepository,
            IPictureService pictureService)
        {
            this._tempDomainService = tempDomainService;
            this._languageService = languageService;
            this._lsrService = lsrService;
            this._langRepository = langRepository;
            this._pictureService = pictureService;
        }

        #endregion

        [UgrinAuthentication]
        public ActionResult Index()
        {
            var lang = this._languageService.GetAllLanguages();

            
            var langs = this._langRepository.Get(null, 1);

            var images = this._pictureService.GetAllPictures();

            return View(images);
        }
        
    }
}