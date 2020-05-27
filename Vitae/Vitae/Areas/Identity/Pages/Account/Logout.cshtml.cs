using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Model.Enumerations;
using Persistency.Repository;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly Repository repository;
        protected readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRequestCultureFeature requestCulture;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, Repository repository, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.repository = repository;
            this.httpContextAccessor = httpContextAccessor;
            requestCulture = signInManager.Context.Features.Get<IRequestCultureFeature>();
        }

        public async Task OnGet()
        {
            await LogAsync();
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await LogAsync();
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }

        private async Task LogAsync()
        {
            var curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            await repository.LogActivityAsync(curriculumID, LogArea.Logout, LogLevel.Information, CodeHelper.GetCalledUri(_signInManager.Context), CodeHelper.GetUserAgent(_signInManager.Context), requestCulture.RequestCulture.UICulture.Name, _signInManager.Context.Connection.RemoteIpAddress.ToString());
        }
    }
}