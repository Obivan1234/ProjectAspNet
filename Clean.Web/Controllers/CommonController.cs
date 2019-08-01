using Clean.Core;
using Clean.Core.Domain.ProductItem;
using Clean.Services.Localization;
using Clean.Services.ProductItem;
using Clean.Web.Localization;
using Newtonsoft.Json.Linq;
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
        #region prop

        private readonly IWebWorkContext _webWorkContext;
        private readonly ILanguageService _languageService;
        private readonly IPictureService _pictureService;

        #endregion

        #region ctor

        public CommonController(IWebWorkContext webWorkContext,
            ILanguageService languageService,
            IPictureService pictureService)
        {
            this._webWorkContext = webWorkContext;
            this._languageService = languageService;
            this._pictureService = pictureService;
        }

        #endregion

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

        [HttpPost]
        public JsonResult AddNewItem(string img64, string mimeType, string description) {
            
            byte[] imgBinary = Convert.FromBase64String(img64);

            this._pictureService.InsertPicture(new Picture() { PictureBinary = imgBinary, Description = description, MimeType = mimeType });
            
            return Json(new { StatusCode = 200 }, JsonRequestBehavior.AllowGet);
        }

    }
}