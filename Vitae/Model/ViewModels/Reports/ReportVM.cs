using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.Reports
{
    public class ReportVM
    {
        public IList<LogVM> Hits { get; set; }
        public IList<LogVM> Logins { get; set; }
    }
}
