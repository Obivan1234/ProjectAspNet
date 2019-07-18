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

        public ActionResult Index()
        {
            int a = 12;
            return View();
        }
        
    }
}