using Clean.Core;
using Clean.Core.Domain.Localization;
using Clean.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clean.Web.Infrastructure
{
    public class WebWorkContext : IWebWorkContext
    {
        //це поле непотрібне так як після присвоєння  йому  значення воно буде не Null для всіх користувачів
        //тому краще мову  стягувати всяий раз з сесії
        //private Language language;

        private readonly ILanguageService _languageService;

        public WebWorkContext(ILanguageService languageService)
        {
            this._languageService = languageService;
        }

        public Language WoorkingLanguage
        {
            get
            {
                var language = HttpContext.Current.Session["currentLanguge"] as Language;

                if (language == null)
                {
                    language = this._languageService.GetAllLanguages().Where(w => w.Default == 0).FirstOrDefault();
                    HttpContext.Current.Session["currentLanguge"] = language ?? throw new ArgumentNullException("there is no default language");
                }
                
                return language;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("attempt  set  workingLanguageToNull");
                else
                    HttpContext.Current.Session["currentLanguge"] = value;
            }
        }
    }
}