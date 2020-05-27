using System;
using System.Collections.Generic;
using System.Text;

namespace Model.ViewModels.Reports
{
    public class ReportVM
    {
        public (IEnumerable<string>, List<List<int>>, IEnumerable<string>) LastHits { get; set; }
        public (IEnumerable<string>, List<List<int>>, IEnumerable<string>) Logins { get; set; }
        public (IEnumerable<string>, List<List<int>>, IEnumerable<string>) SumHits { get; set; }

        public (IEnumerable<string>, List<List<int>>, IEnumerable<string>) CvOverview { get; set; }
    }
}