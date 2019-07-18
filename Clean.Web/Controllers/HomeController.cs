using Clean.Core.Domain.Temp;
using Clean.Data;
using Clean.Services.Temp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Clean.Web.Localization;

namespace Clean.Web.Controllers
{
    [LanguageSeoCode]
    public class HomeController : Controller
    {
        #region prop

        private readonly ITempDomainService _tempDomainService;

        #endregion

        #region ctor

        public HomeController(ITempDomainService tempDomainService)
        {
            this._tempDomainService = tempDomainService;
        }

        #endregion

        public ActionResult Index()
        {
            int a = 12;
            return View();
        }
        
    }
}