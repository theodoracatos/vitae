using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Vitae.Code;
using Vitae.Code.PageModels;

namespace Vitae.Pages
{

    public class IndexModel : LandingPageBaseModel
    {
        private const string PAGE_PARTIAL = "_Content{0}";

        #region AJAX
        public IActionResult OnPostLoadPartial(int id)
        {
            return GetPartialViewResult(string.Format(PAGE_PARTIAL, id));
        }

        #endregion

        #region Helper
        protected PartialViewResult GetPartialViewResult(string viewName, string modelName = "IndexModel", bool hasUnsafedChanges = true)
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

        #endregion
    }
}