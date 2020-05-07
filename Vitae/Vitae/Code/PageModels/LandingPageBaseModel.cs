using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Model.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vitae.Code.PageModels
{

    public class LandingPageBaseModel : PageModel
    {
        public string LanguageCode { get { return GetLanguageCode(); } }

        private string GetLanguageCode()
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var culture = rqf.RequestCulture.UICulture;

            var languageCode = Enum.IsDefined(typeof(ApplicationLanguage), culture.TwoLetterISOLanguageName) ? culture.Name : ApplicationLanguage.en.ToString();

            return languageCode;
        }
    }
}