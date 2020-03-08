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
        public ReportVM Report { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public void OnGet()
        {
            var datapoints = new Dictionary<DateTime, int>()
            {
                { new DateTime(2020,2,8), 1 },
                { new DateTime(2020,2,9), 4 },
                { new DateTime(2020,2,11), 7 },
                { new DateTime(2020,2,16), 3 },
                { new DateTime(2020,2,22), 3 },
                { new DateTime(2020,3,3), 9 },
                { new DateTime(2020,3,6), 12 },
            };

            var datapointsRadar = new List<PointVM>()
            {
                new PointVM(){ Point = 1 },
                new PointVM(){ Point = 2 },
                new PointVM(){ Point = 3 },
                new PointVM(){ Point = 4 },
                new PointVM(){ Point = 5 },
                new PointVM(){ Point = 6 },
                new PointVM(){ Point = 7 },
                new PointVM(){ Point = 8 },
                new PointVM(){ Point = 9 },
                new PointVM(){ Point = 10 },
                new PointVM(){ Point = 11 },
                new PointVM(){ Point = 12 }
            };

            Report = new ReportVM() { LastHits = new List<LogVM>(), Logins = new List<LogVM>(), SumHits = new List<LogVM>(), Points = datapointsRadar };

            for(DateTime currentDate = DateTime.Now.Date.AddDays(-29); currentDate <= DateTime.Now.Date; currentDate = currentDate.AddDays(1))
            {
                Report.LastHits.Add(new LogVM() { LogDate = currentDate, Hits = datapoints.ContainsKey(currentDate) ? datapoints[currentDate] : 0 });
                Report.Logins.Add(new LogVM() { LogDate = currentDate, Hits = datapoints.ContainsKey(currentDate) ? (datapoints[currentDate] + currentDate.Day) : 0 });
            }

            var hits = 0;
            for (int i=200; i>0; i--)
            {
                if(hits % 10 == 0)
                {
                    Report.SumHits.Add(new LogVM() { LogDate = DateTime.Now.Date.AddDays(-i), Hits = hits });
                }
                hits += new Random().Next(0, 10);
            }
        }

        protected override void FillSelectionViewModel()
        {
            
        }
    }
}