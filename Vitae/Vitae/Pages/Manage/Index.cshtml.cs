using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

using System;

using Vitae.Resources;

namespace Vitae
{
    public class IndexModel : PageModel
    {
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