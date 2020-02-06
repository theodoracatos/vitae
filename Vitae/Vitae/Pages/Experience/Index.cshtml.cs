using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Resources;
using Library.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Persistency.Data;

namespace Vitae.Pages.Experience
{
    public class IndexModel : PageModel
    {
        private const string PAGE_EXPERIENCE = "_Experience";
        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [BindProperty]
        public List<ExperienceVM> Experiences { get; set; }

        public int MaxExperiences { get; } = 20;
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


    }
}