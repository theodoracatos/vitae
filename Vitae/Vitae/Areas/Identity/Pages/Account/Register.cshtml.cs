using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

using IEmailSender = Vitae.Code.Mailing.IEmailSender;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : BasePageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(IEmailSender emailSender, SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository) 
        {
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

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
            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.NewPassword), Prompt = nameof(SharedResource.NewPassword))]
            [RegularExpression(Globals.REGEX_PASSWORD, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ConfirmPassword), Prompt = nameof(SharedResource.ConfirmPassword))]
            [Compare("Password", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.CompareFailed))]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid && await base.CheckCaptcha())
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation(SharedResource.UserCreatedWithPassword);

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    var bodyText = await CodeHelper.GetMailBodyTextAsync(SharedResource.ConfirmEmail, Input.Email, new Tuple<string, string, string>(SharedResource.MailAdvert3, callbackUrl, SharedResource.ClickingHere), true, SharedResource.Hello);
                    var logoStream = CodeHelper.GetLogoStream(Globals.LOGO);
                    var message = new Message(new string[] { Input.Email }, SharedResource.ConfirmEmail, bodyText, new FormFileCollection() { new FormFile(logoStream, 0, logoStream.Length, "image/png", "logo") });
                    await _emailSender.SendEmailAsync(message);

                    if (userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
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