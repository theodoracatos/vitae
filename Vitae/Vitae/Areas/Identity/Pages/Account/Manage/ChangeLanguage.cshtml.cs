using Library.Helper;
using Library.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistency.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Vitae.Areas.Identity.Pages.Account.Manage
{
    public class ChangeLanguageModel : PageModel
    {
        private readonly VitaeContext vitaeContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly Guid curriculumID;

        public ChangeLanguageModel(
            VitaeContext vitaeContext,
            IHttpContextAccessor httpContextAccessor,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<ChangePasswordModel> logger)
        {
            this.vitaeContext = vitaeContext;
            this.curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        public string ApplicationLanguageCode { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public bool Success { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{SharedResource.UnableToLoadUserID} '{_userManager.GetUserId(User)}'.");
            }

            ApplicationLanguageCode = vitaeContext.Curriculums.Include(c => c.Language)
                .Single(c => c.CurriculumID == curriculumID).Language.LanguageCode;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{SharedResource.UnableToLoadUserID} '{_userManager.GetUserId(User)}'.");
            }

            vitaeContext.Curriculums.Single(c => c.CurriculumID == curriculumID).Language = vitaeContext.Languages.Single(l => l.LanguageCode == ApplicationLanguageCode);
            var cultureInfo = new CultureInfo(ApplicationLanguageCode, false);

            // Set cookie value
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureInfo)),
                new CookieOptions 
                { 
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    SameSite = SameSiteMode.Lax,
                    Secure = true
                }
            );

            await vitaeContext.SaveChangesAsync();
            Success = true;

            return Redirect("/Manage");
        }
    }
}
