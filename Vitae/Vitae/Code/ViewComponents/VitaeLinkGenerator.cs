using Library.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Persistency.Data;

using System.Linq;

namespace Vitae.Code.ViewComponents
{
    public class VitaeLinkGenerator : ViewComponent
    {
        private readonly VitaeContext vitaeContext;
        private readonly IHttpContextAccessor httpContextAccessor;

        public VitaeLinkGenerator(IHttpContextAccessor httpContextAccessor, VitaeContext vitaeContext)
        {
            this.vitaeContext = vitaeContext;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            var languageCodes = vitaeContext.CurriculumLanguages.Where(cl => cl.CurriculumID == curriculumID).OrderBy(c => c.Order).Select(c => c.Language.LanguageCode);
  
            return View("VitaeLinkGenerator", languageCodes);
        }
    }
}