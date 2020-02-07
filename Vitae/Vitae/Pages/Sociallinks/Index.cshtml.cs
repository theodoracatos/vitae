using Library.Enumerations;
using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

namespace Vitae.Pages.Sociallinks
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_SOCIALLINKS = "_Sociallinks";
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SocialLinks), Prompt = nameof(SharedResource.SocialLinks))]
        [BindProperty]
        public IList<SocialLinkVM> SocialLinks { get; set; }

        public int MaxSocialLinks { get; } = Enum.GetNames(typeof(SocialPlatform)).Length;

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();

                SocialLinks = curriculum.Person.SocialLinks.OrderBy(ls => ls.Order)
                    .Select(s => new SocialLinkVM()
                    {
                        Link = s.Link,
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform
                    }).ToList();

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = GetCurriculum();
                appContext.RemoveRange(curriculum.Person.SocialLinks);

                curriculum.Person.SocialLinks =
                    SocialLinks.Select(s => new Poco.SocialLink()
                    {
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform,
                        Link = s.Link
                    }).ToList();

                await appContext.SaveChangesAsync();
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
            if (SocialLinks.Count == 0)
            {
                SocialLinks.Add(new SocialLinkVM() { Order = 1 });
            }
            else if (SocialLinks.Count < MaxSocialLinks)
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

        private Curriculum GetCurriculum()
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.SocialLinks)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {

        }

        #endregion
    }
}