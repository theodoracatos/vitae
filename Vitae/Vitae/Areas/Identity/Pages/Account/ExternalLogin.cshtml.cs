using Library.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : BasePageModel
    {
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(IEmailSender emailSender, SignInManager<IdentityUser> signInManager, ILogger<ExternalLoginModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender)
        {
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Range(typeof(bool), "true", "true", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.TermsAndConditionsError))]
            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.TermsAndConditionsAccept), Prompt = nameof(SharedResource.TermsAndConditionsAccept))]
            public bool ConfirmAgreements { get; set; }
        }

        public IActionResult OnGetAsync()
        { 
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl });
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor : true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect("~/Manage");
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            // Get the information about the user from the external login provider
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        // CV
                        await base.CreateCurriculumAndSignInAsync(user);
                        await base.SendRegistrationMailAsync(SharedResource.AccountCreated, email, loginPrivider: info.LoginProvider);

                        return LocalRedirect("~/Manage");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;
            return Page();
        }

        protected override void FillSelectionViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
