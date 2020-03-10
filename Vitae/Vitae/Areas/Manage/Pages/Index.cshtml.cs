using Microsoft.AspNetCore.Mvc.RazorPages;

using Model.ViewModels.Reports;

using System;
using System.Linq;
using System.Collections.Generic;
using Vitae.Code;
using Microsoft.Extensions.Localization;
using Persistency.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Library.Resources;
using Library.Repository;

namespace Vitae.Areas.Manage.Pages
{
    public class IndexModel : BasePageModel
    {
        private const int DAYS = 30;

        public ReportVM Report { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public void OnGet()
        {
            FillSelectionViewModel();
        }

        protected override void FillSelectionViewModel()
        {
            Report = new ReportVM()
            {
                LastHits = repository.GetHits(curriculumID, DAYS),
                Logins = repository.GetLogins(curriculumID, DAYS),
                SumHits = repository.GetHits(curriculumID)
            };
        }
    }
}