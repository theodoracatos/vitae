using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Persistency.Data;
using Persistency.Repository;

using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : BasePageModel
    {
        public ConfirmEmailModel(IEmailSender emailSender, SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender)
        {}

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
                    await base.CreateCurriculumAndSignInAsync(user);
                }
            }
            else
            {
                StatusMessage = SharedResource.EmailAlreadyConfirmed;
            }

            return Page();
        }

        protected override void FillSelectionViewModel()
        {
            throw new NotImplementedException();
        }
    }
}