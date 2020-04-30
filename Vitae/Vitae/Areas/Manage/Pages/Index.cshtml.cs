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
        private const int HITS = 100;
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
            var lastHits = repository.GetHits(curriculumID, days: DAYS);
            var lastHits_Dates = lastHits.OrderBy(l => l.LogDate).Select(l => l.LogDate.ToString("dd.MM")).Distinct();
            var lastHits_PublicationIds = lastHits.OrderBy(l => l.LogDate).Select(l => l.PublicationID).Distinct();

            var lastHitsHits = new List<List<int>>();
            foreach (var pubId in lastHits_PublicationIds)
            {
                var filteredHits = lastHits.Where(l => l.PublicationID == pubId);
                lastHitsHits.Add(GetHits(lastHits_Dates, filteredHits, pubId));
            }

            Report = new ReportVM()
            {
                LastHits = (lastHits_Dates, lastHitsHits, lastHits_PublicationIds),
                Logins = repository.GetLogins(curriculumID, DAYS),
                SumHits = repository.GetHits(curriculumID, hits: HITS)
            };
        }

        private List<int> GetHits(IEnumerable<string> dates, IEnumerable<LogVM> hits, Guid guid)
        {
            var hitlist = new List<int>(); 

            foreach(var date in dates)
            {
                if(hits.Any(h => h.LogDate.ToString("dd.MM") == date))
                {
                    hitlist.Add(hits.Single(h => h.LogDate.ToString("dd.MM") == date).Hits);
                }
                else
                {
                    hitlist.Add(0);
                }
            }

            return hitlist;
        }
    }
}