using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CVitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : PageModel
    {
        public Guid Guid { get; set; }

        public void OnGet(Guid id)
        {
            Guid = id;
        }
    }
}
