using Library.Constants;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace Vitae.Pages
{
    public class XmlSitemapModel : PageModel
    {
        public Dictionary<string, DateTime> VitaePages { get; private set; } = new Dictionary<string, DateTime>()
        {
            { $"{Globals.APPLICATION_URL}", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/{Globals.GENERAL_INFORMATION}", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/{nameof(Areas.Manage)}", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/{nameof(Areas.CV)}", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/{nameof(Areas.Identity)}", DateTime.Now }
        };

        public void OnGet()
        {

        }
    }
}