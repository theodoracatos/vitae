using Library.Extensions;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Model.Poco;
using Model.ViewModels;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code.PageModels;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Awards
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_AWARDS = "_Awards";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Awards), Prompt = nameof(SharedResource.Awards))]
        [BindProperty]
        public IList<AwardVM> Awards { get; set; }

        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxAwards { get; } = 20;

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
                var curriculum = await repository.GetCurriculumAsync<Award>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadAwards(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Award>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Awards.Where(a => a.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Awards.Select(a => new Poco.Award()
                    {
                        AwardedFrom = a.AwardedFrom,
                        AwardedOn = new DateTime(a.Year, a.Month, 1),
                        Description = a.Description,
                        Link = a.Link,
                        Name = a.Name,
                        Order = a.Order,
                        CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                    }).ToList().ForEach(a => curriculum.Awards.Add(a));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddAward()
        {
            if (Awards.Count < MaxAwards)
            {
                Awards.Add(new AwardVM() 
                { 
                    Order = Awards.Count,
                    Collapsed = base.Collapsed
                });
                Awards = CheckOrdering(Awards);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostRemoveAward()
        {
            Remove(Awards);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostUpAward(int order)
        {
            Up(Awards, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostDownAward(int order)
        {
            Down(Awards, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public async Task<IActionResult> OnPostDeleteAwardAsync(int order)
        {
            Delete(Awards, order);

            var curriculum = await repository.GetCurriculumAsync<Award>(curriculumID);

            var item = curriculum.Awards.SingleOrDefault(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order == order);
            if (item != null)
            {
                vitaeContext.Remove(item);
                curriculum.Awards.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order > order).ToList().ForEach(e => e.Order = e.Order - 1);

                await vitaeContext.SaveChangesAsync();
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostCopyAward(int order)
        {
            if (Awards.Count < MaxAwards)
            {
                var copy = Awards[order].DeepClone();
                copy.Order = Awards.Count;
                Awards.Add(copy);
                Awards = CheckOrdering(Awards);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Awards);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadAwards(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadAwards(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Award>(curriculumID);

            Awards = repository.GetAwards(curriculum, languageCode);
        }

        #endregion
    }
}