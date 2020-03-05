using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Model.ViewModels.Reports;
using Newtonsoft.Json;
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
            Report = new ReportVM()
            {
                Logs = new List<LogVM>()
                 {
                    new LogVM(){ Hits = 10, LogDate = new DateTime(2020,2,1) },
                    new LogVM(){ Hits = 3, LogDate = new DateTime(2020,2,3) },
                    new LogVM(){ Hits = 5, LogDate = new DateTime(2020,2,4) },
                    new LogVM(){ Hits = 21, LogDate = new DateTime(2020,2,19) },
                    new LogVM(){ Hits = 2, LogDate = new DateTime(2020,3,1) },
                    new LogVM(){ Hits = 4, LogDate = new DateTime(2020,3,4) }
                 }
            };
        }
    }
}