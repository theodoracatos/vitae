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
            { $"{Globals.APPLICATION_URL}{Globals.GENERAL_INFORMATION_LINK}", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/Identity/Account/Login", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/Identity/Account/Register", DateTime.Now },
            { $"{Globals.APPLICATION_URL}/Identity/Account/ForgotPassword", DateTime.Now },
        };

        public void OnGet()
        {

        }
    }
}