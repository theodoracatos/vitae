using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.Reports
{
    public class ReportVM
    {
        public IList<LogVM> LastHits { get; set; }
        public IList<LogVM> SumHits { get; set; }
        public IList<LogVM> Logins { get; set; }
        public IList<PointVM> Points { get; set; }
    }
}