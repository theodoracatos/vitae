using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

using System;

namespace Vitae
{
    public class IndexModel : PageModel
    {
        public PersonVM PersonVM { get; set; }

        private readonly IStringLocalizer<SharedResource> localizer;

        public IndexModel(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }

        public IActionResult OnGet(Guid id)
        {
            if(id == Guid.Empty)
            {
                return NotFound();
            }

            return Page();
        }
    }
}