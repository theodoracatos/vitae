using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vitae.Pages.Manage
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public PersonVM Person { get; set; }
        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
        public string PhonePrefix { get; set; }

        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        public IActionResult OnGet(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            FillViewModel();

            return Page();
        }

        #region SYNC

        public IActionResult OnPostSave()
        {
            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostChangeCountry()
        {
            FillViewModel();
            ModelState.Clear();

            return GetPartialViewResult("_Index_Personal");
        }

        #endregion

        #region Helper
        private void FillViewModel()
        {
            Countries = appContext.Countries.OrderBy(c => c.Name).Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.Culture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.Culture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.Culture.Name == "es" ? c.Name_es :
                        c.Name,
                PhoneCode = c.PhoneCode
            });

            Languages = appContext.Languages.OrderBy(c => c.Name).Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.Culture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.Culture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.Culture.Name == "es" ? c.Name_es :
                        c.Name
            });

            PhonePrefix = !string.IsNullOrEmpty(Person?.CountryCode) ? "+" + Countries.Where(c => c.CountryCode == Person.CountryCode).Select(c => c.PhoneCode).Single().ToString() : string.Empty;
        }
        private PartialViewResult GetPartialViewResult(string viewName)
        {
            // Ajax
            var dataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { nameof(IndexModel), this } };
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