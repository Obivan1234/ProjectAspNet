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
using Clean.Web.Filter;
using Clean.Core.Domain.ApplicationUser;
using Clean.Core.Data;
using Clean.Services.ProductItem;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Clean.Services.ApplicationUser;
using System.Threading.Tasks;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class HomeController : BaseController
    {
        #region prop

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

        private readonly ITempDomainService _tempDomainService;
        private readonly ILanguageService _languageService;
        private readonly ILocalStringResourceService _lsrService;
        private readonly IRepository<Language> _langRepository;
        private readonly IPictureService _pictureService;
        private readonly ILoginModelService _loginModelService;

        #endregion

        #region ctor

        public HomeController(ITempDomainService tempDomainService,
            ILanguageService languageService,
            ILocalStringResourceService lsrService,
            IRepository<Language> langRepository,
            IPictureService pictureService,
            ILoginModelService loginModelService)
        {
            this._tempDomainService = tempDomainService;
            this._languageService = languageService;
            this._lsrService = lsrService;
            this._langRepository = langRepository;
            this._pictureService = pictureService;
            this._loginModelService = loginModelService;
        }

        //public HomeController(IPictureService pictureService)
        //{
        //    this._pictureService = pictureService;
        //}

        #endregion

        [UgrinAuthentication]
        public ActionResult Index()
        {

            var lang = this._languageService.GetAllLanguages();

            var langs = this._langRepository.Get(null, 1);

            var userId = User.Identity.GetUserId();
            var userName = User.Identity.Name;

            ApplicationUser appUser = UserManager.FindById(userId);

            ViewBag.UserName = userName;
            ViewBag.Description = appUser.Description;


            // Get image path  
            string imgPath = Server.MapPath("~/Content/img/default-user.png");
            // Convert image to byte array  
            byte[] byteData = System.IO.File.ReadAllBytes(imgPath);
            string imreBase64Data;

            if (appUser.imageData == null)
            {
                imreBase64Data = Convert.ToBase64String(byteData);
            }
            else
            {
                imreBase64Data = Convert.ToBase64String(appUser.imageData);
            }
            
            string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
  
            ViewBag.ImageData = imgDataURL;


            var images = this._pictureService.GetPicturesByUserIdDesc(userId, 8);

            return View(images);
        }
        
    }
}