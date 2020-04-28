using Library.Constants;
using Library.Helper;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using Model.Enumerations;

using System;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly Repository repository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IRequestCultureFeature requestCulture;
        private readonly HttpContext httpContext;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, Repository repository, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.repository = repository;
            this.signInManager = signInManager;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
            httpContext = httpContextAccessor.HttpContext;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public bool Succeeded { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"{SharedResource.UnableToLoadUserID} '{userId}'.");
            }

            if (!user.EmailConfirmed)
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                var result = await userManager.ConfirmEmailAsync(user, code);
                StatusMessage = result.Succeeded ? SharedResource.ConfirmEmailThankYou : SharedResource.ErrorConfirmEmail;
                Succeeded = result.Succeeded;

                if (result.Succeeded)
                {
                    // CV
                    var curriculumID = await repository.AddCurriculumAsync(Guid.Parse(user.Id), requestCulture.RequestCulture.UICulture.Name);
                    await repository.LogAsync(curriculumID, LogArea.Login, LogLevel.Information, CodeHelper.GetCalledUri(httpContext), CodeHelper.GetUserAgent(httpContext), requestCulture.RequestCulture.UICulture.Name, httpContext.Connection.RemoteIpAddress.ToString());

                    // Language
                    Response.Cookies.Append(
                       CookieRequestCultureProvider.DefaultCookieName,
                       CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(new CultureInfo(requestCulture.RequestCulture.UICulture.Name))),
                       new CookieOptions 
                       { 
                           Expires = DateTimeOffset.UtcNow.AddYears(1),
                           SameSite = SameSiteMode.Lax,
                           Secure = true
                       }
                   );

                    // Role & Claims
                    await userManager.AddToRoleAsync(user, Roles.USER);
                    var claim = new Claim(Claims.CURRICULUM_ID, curriculumID.ToString());
                    await userManager.AddClaimAsync(user, claim);

                    await signInManager.SignInAsync(user, isPersistent: false);
                }
            }
            else
            {
                StatusMessage = SharedResource.EmailAlreadyConfirmed;
            }

            return Page();
        }
    }
}