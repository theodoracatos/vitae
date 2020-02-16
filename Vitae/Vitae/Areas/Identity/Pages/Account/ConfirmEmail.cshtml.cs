using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Persistency.Data;
using Persistency.Poco;

namespace Vitae.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly VitaeContext vitaeContext;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, VitaeContext vitaeContext)
        {
            _userManager = userManager;
            this.vitaeContext = vitaeContext;
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
             
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"{SharedResource.UnableToLoadUserID} '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? SharedResource.ConfirmEMailThankYou : SharedResource.ErrorConfirmEMail;
            Succeeded = result.Succeeded;

            if (result.Succeeded)
            {
                var guid = Guid.NewGuid();
                vitaeContext.Curriculums.Add(
                    new Curriculum()
                    {
                        Identifier = guid,
                        ShortIdentifier = Convert.ToBase64String(guid.ToByteArray()),
                        UserID = Guid.Parse(user.Id),
                        CreatedOn = DateTime.Now,
                        FriendlyId = Convert.ToBase64String(guid.ToByteArray()),
                        LastUpdated = DateTime.Now
                    }
                );

                await vitaeContext.SaveChangesAsync();
            }

            return Page();
        }
    }
}
