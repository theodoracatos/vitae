using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Model.Enumerations;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BasePageModel
    {
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IEmailSender emailSender, SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender)
        {
            _logger = logger;
        }


        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Email), Prompt = nameof(SharedResource.Email))]
            [EmailAddress(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
            [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
            public string Email { get; set; }

            [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
            [StringLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.PasswordErrorLength), MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Password), Prompt = nameof(SharedResource.Password))]
            public string Password { get; set; }

            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.RememberMeDesc), Prompt = nameof(SharedResource.RememberMeDesc))]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Manage");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByNameAsync(Input.Email);
                    var claims = await userManager.GetClaimsAsync(user);
                    var curriculumID = Guid.Parse(claims.Single(c => c.Type == Claims.CURRICULUM_ID).Value);
                    _logger.LogInformation(SharedResource.UserLoggedIn);

                    await repository.LogActivityAsync(curriculumID, LogArea.Login, LogLevel.Information, CodeHelper.GetCalledUri(signInManager.Context), CodeHelper.GetUserAgent(signInManager.Context), requestCulture.RequestCulture.UICulture.Name, signInManager.Context.Connection.RemoteIpAddress.ToString());

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning(SharedResource.AccountLockedOut);
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, SharedResource.InvalidLoginAttempt);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        protected override void FillSelectionViewModel()
        {
            throw new NotImplementedException();
        }
    }
}
