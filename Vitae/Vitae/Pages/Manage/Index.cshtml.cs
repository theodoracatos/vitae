using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Vitae
{
    public class IndexModel : PageModel
    {
        public PersonVM Person { get; set; }
        public IEnumerable<CountryVM> Countries { get; set; }

        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext)
        {
            this.localizer = localizer;
            this.appContext = appContext;
        }

        public IActionResult OnGet(Guid id)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }

            Countries = appContext.Countries.Select(c => new CountryVM() { CountryCode = c.CountryCode, Name = c.Name });

            return Page();
        }
    }
}