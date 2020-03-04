using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Model.Enumerations;
using Model.ViewModels;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Sociallinks
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_SOCIALLINKS = "_Sociallinks";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SocialLinks), Prompt = nameof(SharedResource.SocialLinks))]
        [BindProperty]
        public IList<SocialLinkVM> SocialLinks { get; set; }

        public int MaxSocialLinks { get; } = Enum.GetNames(typeof(SocialPlatform)).Length;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                SocialLinks = repository.GetSocialLinks(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.SocialLinks);

                curriculum.Person.SocialLinks =
                    SocialLinks.Select(s => new Poco.SocialLink()
                    {
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform,
                        Link = s.Link
                    }).ToList();
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostSelectChange()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostAddSocialLink()
        {
            if (SocialLinks.Count < MaxSocialLinks)
            {
                SocialLinks.Add(new SocialLinkVM() { Order = SocialLinks.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostRemoveSocialLink()
        {
            if (SocialLinks.Count > 0)
            {
                SocialLinks.RemoveAt(SocialLinks.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostUpSocialLink(int order)
        {
            var socialLink = SocialLinks[order];
            SocialLinks[order] = SocialLinks[order - 1];
            SocialLinks[order - 1] = socialLink;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostDownSocialLink(int order)
        {
            var socialLink = SocialLinks[order];
            SocialLinks[order] = SocialLinks[order + 1];
            SocialLinks[order + 1] = socialLink;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }
        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {}

        #endregion
    }
}