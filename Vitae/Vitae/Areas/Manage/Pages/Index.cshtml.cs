using Microsoft.AspNetCore.Mvc.RazorPages;

using Model.ViewModels.Reports;

using System;
using System.Collections.Generic;

namespace Vitae.Areas.Manage.Pages
{
    public class IndexModel : PageModel
    {
        public ReportVM Report { get; set; }

        public IndexModel()
        {
        }

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

            Report = new ReportVM() { Hits = new List<LogVM>(), Logins = new List<LogVM>() };

            for(DateTime currentDate = DateTime.Now.Date.AddDays(-29); currentDate <= DateTime.Now.Date; currentDate = currentDate.AddDays(1))
            {
                Report.Hits.Add(new LogVM() { LogDate = currentDate, Hits = datapoints.ContainsKey(currentDate) ? datapoints[currentDate] : 0 });
                Report.Logins.Add(new LogVM() { LogDate = currentDate, Hits = datapoints.ContainsKey(currentDate) ? datapoints[currentDate] + currentDate.Day : 0 });

            }
        }
    }
}