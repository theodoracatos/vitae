using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Vitae.Areas.Manage.Pages
{
    public class ChartData
    {
        public int[] Data_Axis_X { get; set; }
        public int[] Data_Axis_Y { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ChartData ChartData { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ChartData = new ChartData()
            {
                Data_Axis_X = new int[] { 1997, 2003, 2005, 2009, 2014, 2018, 2019 },
                Data_Axis_Y = new int[] { 1, 3, 5, 3, 1, 18, 9 }
            };
        }
    }
}