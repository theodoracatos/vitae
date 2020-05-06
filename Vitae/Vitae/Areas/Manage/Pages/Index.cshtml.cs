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
using Model.Poco;
using Microsoft.AspNetCore.Localization;

namespace Vitae.Areas.Manage.Pages
{
    public class IndexModel : BasePageModel
    {
        private const int HITS = 50;
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
                LastHits = GetLastHits(filterDays: true),
                Logins = GetLastLogins(),
                SumHits = GetLastHits(filterDays: false),
                CvOverview = GetCvOverview()
            };
        }

        private (List<string> labels, List<int> values) GetCvOverview()
        {
            var curriculumLanguage = (repository.GetCurriculumAsync<PersonalDetail>(curriculumID).Result).CurriculumLanguages.First().Language.LanguageCode;
            var curriculumItems = repository.CountItemsFromCurriculumLanguageAsync(curriculumID, curriculumLanguage).Result;
            var labels = new List<string>();
            var values = new List<int>();

            foreach (var item in curriculumItems.Where(i => i.Key != SharedResource.About && i.Key != SharedResource.PersonalDetails))
            {
                labels.Add(item.Key);
                values.Add(item.Value);
            }

            return (labels, values);
        }

        private (IEnumerable<string> dates, List<List<int>> hits, IEnumerable<string> labels) GetLastLogins()
        {
            var lastLogins = repository.GetLogins(curriculumID, DAYS);
            var loginDates = new List<string>();

            // Hits
            var lastLoginsHits = new List<List<int>>();
            var lastHits = new List<int>();
            var startDate = DateTime.Now.Date.AddDays(-DAYS);
            for (int i = 0; i < DAYS; i++)
            {
                var currentDate = startDate.AddDays(i + 1);
                if(lastLogins.Select(l => l.LogDate.Date).Contains(currentDate))
                {
                    lastHits.Add(lastLogins.Where(l => l.LogDate.Date == currentDate).Sum(l => l.Hits));
                }
                else
                {
                    lastHits.Add(0);
                }

                // Dates
                loginDates.Add(currentDate.ToString("dd.MM"));
            }
            lastLoginsHits.Add(lastHits);

            // Labels
            var labels = new List<string>() { "Login" }.AsEnumerable();
            return (dates: loginDates, lastLoginsHits, labels);
        }

        private (IEnumerable<string> dates, List<List<int>> hits, IEnumerable<string> labels) GetLastHits(bool filterDays)
        {
            var lastHits = filterDays ? repository.GetHits(curriculumID, days: DAYS) : repository.GetHits(curriculumID, hits: HITS);
            var lastHits_Dates = filterDays ? GetDateRange(DateTime.Now.Date.AddDays(-DAYS), DAYS) : lastHits.OrderBy(l => l.LogDate).Select(l => l.LogDate.ToString("dd.MM")).Distinct();
            var lastHits_PublicationIds = lastHits.OrderByDescending(l => l.LogDate).Select(l => l.PublicationID).Distinct();

            // Hits
            var lastHitsHits = new List<List<int>>();
            foreach (var pubId in lastHits_PublicationIds)
            {
                var filteredHits = lastHits.Where(l => l.PublicationID == pubId);
                lastHitsHits.Add(GetHitsByPublicationID(lastHits_Dates, filteredHits, pubId));
            }

            // Labels
            var lastHitsLabels = lastHits_PublicationIds.Select(p => p.ToString().Substring(0, p.ToString().IndexOf("-")));

            if (lastHitsHits.Count == 0)
            {
                lastHitsHits.Add(Enumerable.Range(1, 30).Select(n => 0).ToList());
                lastHitsLabels = new List<string>() { SharedResource.NoHits };
            }

            return (dates: lastHits_Dates, hits: lastHitsHits, labels: lastHitsLabels);
        }

        private List<string> GetDateRange(DateTime startDate, int days)
        {
            var dates = new List<string>();
            for (int i = 0; i < days; i++)
            {
                var currentDate = startDate.AddDays(i + 1);
                dates.Add(currentDate.ToString("dd.MM"));
            }

            return dates;
        }

        private List<int> GetHitsByPublicationID(IEnumerable<string> dates, IEnumerable<LogVM> hits, Guid publicationID)
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