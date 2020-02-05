using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using Vitae.Code;

namespace Vitae.Pages.Education
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_EDUCATION = "_Education";
        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [BindProperty]
        public List<EducationVM> Educations { get; set; }

        public int MaxEducations { get; } = 20;
        public int YearStart { get; } = 1900;

        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        public IEnumerable<MonthVM> Months { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            Educations = new List<EducationVM>() { new EducationVM() { Order = 1 } };

            FillSelectionViewModel();
            return Page();
        }

        #endregion

        #region AJAX
        public IActionResult OnPostAddEducation()
        {
            if (Educations == null)
            {
                Educations = new List<EducationVM>() { new EducationVM() { Order = 1 } };
            }
            else if (Educations.Count < MaxEducations)
            {
                Educations.Add(new EducationVM() { Order = Educations.Count + 1 });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostRemoveEducation()
        {
            if (Educations == null)
            {
                Educations = new List<EducationVM>() { new EducationVM() { Order = 1 } };
            }
            else if (Educations.Count > 1)
            {
                Educations.RemoveAt(Educations.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPost()
        {

            return Page();
        }
        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Months = appContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
                requestCulture.RequestCulture.Culture.Name == "fr" ? c.Name_fr :
                requestCulture.RequestCulture.Culture.Name == "it" ? c.Name_it :
                requestCulture.RequestCulture.Culture.Name == "es" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);
        }
        #endregion
    }
}