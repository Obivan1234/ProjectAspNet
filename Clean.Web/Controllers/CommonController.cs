using Clean.Core;
using Clean.Services.Localization;
using Clean.Web.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class CommonController : Controller
    {
        private readonly IWebWorkContext _webWorkContext;
        private readonly ILanguageService _languageService;


        public CommonController(IWebWorkContext webWorkContext,
            ILanguageService languageService)
        {
            this._webWorkContext = webWorkContext;
            this._languageService = languageService;
        }


        public ActionResult LanguageSelector()
        {
            var language = this._webWorkContext.WoorkingLanguage;

            ViewBag.AllLanguages = this._languageService.GetAllLanguages();

            return PartialView("_LanguageSelector", language);
        }

        [HttpPost]
        public JsonResult SetLanguage(int langId)
        {
            var language = this._languageService.GetLanguageById(langId);

            if (language != null && language.Published)
                this._webWorkContext.WoorkingLanguage = language;

            return Json(new { StatusCode = 200 }, JsonRequestBehavior.AllowGet);

        }

    }
}