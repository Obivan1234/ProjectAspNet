using Clean.Core;
using Clean.Core.Domain.ApplicationUser;
using Clean.Core.Domain.ProductItem;
using Clean.Core.Domain.Temp;
using Clean.Data;
using Clean.Services.ApplicationUser;
using Clean.Services.Localization;
using Clean.Services.ProductItem;
using Clean.Web.Localization;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class CommonController : BaseController
    {
        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        #region prop

        private readonly IWebWorkContext _webWorkContext;
        private readonly ILanguageService _languageService;
        private readonly IPictureService _pictureService;
        private readonly ILoginModelService _loginModelService;

        #endregion

        #region ctor

        public CommonController(IWebWorkContext webWorkContext,
            ILanguageService languageService,
            IPictureService pictureService,
            ILoginModelService loginModelService)
        {
            this._webWorkContext = webWorkContext;
            this._languageService = languageService;
            this._pictureService = pictureService;
            this._loginModelService = loginModelService;
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
        public async Task<JsonResult> AddNewItem(string img64, string mimeType, string description)
        {
            
            byte[] imgBinary = Convert.FromBase64String(img64);

            var model = _loginModelService.GetAllLogins().FirstOrDefault();

            ApplicationUser applicationUser = await UserManager.FindAsync(model.UserName, model.Password);

            this._pictureService.InsertPicture(new Picture() {
                PictureBinary = imgBinary,
                Description = description,
                MimeType = mimeType,
                ApplicationUserMyId = applicationUser.Id });

            
            return Json(new { StatusCode = 200 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AllItemPageProducts(int itemPerPage, int minIdOfItems ) {

            var model = _loginModelService.GetAllLogins().FirstOrDefault();

            ApplicationUser appUser = await UserManager.FindAsync(model.UserName, model.Password);


            var pictures = this._pictureService.GetPicturesByUserIdDesc(appUser.Id, itemPerPage, minIdOfItems);
            
            var content = RenderPartialViewToString("_Sliced_Item", pictures);

            var isLoaded = pictures.Count() == itemPerPage ? false : true;

            var jsonResult = Json(new
            {
                StatusCode = 200,
                Content = content,
                AllContantLoaded = isLoaded,
                MinItemsId =  isLoaded? -1: pictures.Min(p => p.Id)
            });

            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;

        }

    }
}