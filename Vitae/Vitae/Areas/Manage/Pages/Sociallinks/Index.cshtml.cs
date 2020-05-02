using Library.Extensions;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Model.Enumerations;
using Model.Poco;
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
        public const string PAGE_SOCIALLINKS = "_Sociallinks";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SocialLinks), Prompt = nameof(SharedResource.SocialLinks))]
        [BindProperty]
        public IList<SocialLinkVM> SocialLinks { get; set; }

        public int MaxSocialLinks { get; } = Enum.GetNames(typeof(SocialPlatform)).Length;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync<SocialLink>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadSocialLinks(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<SocialLink>(curriculumID);
                vitaeContext.RemoveRange(curriculum.SocialLinks.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                SocialLinks.Select(s => new Poco.SocialLink()
                    {
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform,
                        Link = s.Link,
                        CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                    }).ToList().ForEach(s => curriculum.SocialLinks.Add(s));
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
                SocialLinks.Add(new SocialLinkVM() 
                { 
                    Order = SocialLinks.Count,
                    Collapsed = base.Collapsed
                });
                SocialLinks = CheckOrdering(SocialLinks);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostRemoveSocialLink()
        {
            Remove(SocialLinks);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostUpSocialLink(int order)
        {
            Up(SocialLinks, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostDownSocialLink(int order)
        {
            Down(SocialLinks, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public async Task<IActionResult> OnPostDeleteSocialLinkAsync(int order)
        {
            Delete(SocialLinks, order);

            var curriculum = await repository.GetCurriculumAsync<SocialLink>(curriculumID);

            var item = curriculum.SocialLinks.SingleOrDefault(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order == order);
            if (item != null)
            {
                vitaeContext.Remove(item);
                curriculum.SocialLinks.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order > order).ToList().ForEach(e => e.Order = e.Order - 1);

                await vitaeContext.SaveChangesAsync();
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostCopySocialLink(int order)
        {
            if (SocialLinks.Count < MaxSocialLinks)
            {
                var copy = SocialLinks[order].DeepClone();
                copy.Order = SocialLinks.Count;
                SocialLinks.Add(copy);
                SocialLinks = CheckOrdering(SocialLinks);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(SocialLinks);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadSocialLinks(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SOCIALLINKS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadSocialLinks(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<SocialLink>(curriculumID);

            SocialLinks = repository.GetSocialLinks(curriculum, languageCode);
        }

        #endregion
    }
}