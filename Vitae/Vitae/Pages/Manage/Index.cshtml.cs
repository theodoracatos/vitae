using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vitae
{
    public class IndexModel : PageModel
    {
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