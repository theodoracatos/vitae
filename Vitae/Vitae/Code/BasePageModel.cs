using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Vitae.Code
{
    public abstract class BasePageModel : PageModel
    {
        protected PartialViewResult GetPartialViewResult(string viewName, string modelName = "IndexModel")
        {
            // Ajax
            var dataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { modelName, this } };
            dataDictionary.Model = this;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = viewName,
                ViewData = dataDictionary,
            };

            return result;
        }
    }
}