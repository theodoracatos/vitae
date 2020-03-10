﻿using Library.Constants;
using Library.Resources;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using Model.Poco;

using Persistency.Data;

using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly VitaeContext vitaeContext;
        private readonly SignInManager<IdentityUser> signInManager;
        protected readonly IRequestCultureFeature requestCulture;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, VitaeContext vitaeContext, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.vitaeContext = vitaeContext;
            this.signInManager = signInManager;
            this.requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
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
                    var curriculum = new Curriculum()
                    {
                        ShortIdentifier = (DateTime.Now.Ticks - new DateTime(2020, 1, 1).Ticks).ToString("x"),
                        UserID = Guid.Parse(user.Id),
                        CreatedOn = DateTime.Now,
                        FriendlyId = (DateTime.Now.Ticks - new DateTime(2020, 1, 1).Ticks).ToString("x"),
                        LastUpdated = DateTime.Now,
                        Person = new Person() { Language = requestCulture.RequestCulture.UICulture.Name }
                    };
                    vitaeContext.Curriculums.Add(curriculum);
                    await vitaeContext.SaveChangesAsync();

                    // Role & Claims
                    await userManager.AddToRoleAsync(user, Roles.USER);
                    var claim = new Claim(Claims.CV_IDENTIFIER, curriculum.CurriculumID.ToString());
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
