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

using Vitae.Code;
using Vitae.Code.PageModels;
using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Interests
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_INTERESTS = "_Interests";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Interests), Prompt = nameof(SharedResource.Interests))]
        [BindProperty]
        public IList<InterestVM> Interests { get; set; }

        public int MaxInterests { get; } = 20;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync<Interest>(curriculumID);
                LoadLanguageCode(curriculum);

                await LoadInterests(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Interest>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Interests.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Interests.Select(i => new Poco.Interest()
                    {
                        Description = i.Description,
                        Association = i.Association,
                        Link = i.Link,
                        InterestName = i.InterestName,
                        Order = i.Order,
                        CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                    }).ToList().ForEach(i => curriculum.Interests.Add(i));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddInterest()
        {
            if (Interests.Count < MaxInterests)
            {
                Interests.Add(new InterestVM()
                { 
                    Order = Interests.Count,
                    Collapsed = base.Collapsed
                });
                Interests = CheckOrdering(Interests);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostRemoveInterest()
        {
            Remove(Interests);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostUpInterest(int order)
        {
            Up(Interests, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostDownInterest(int order)
        {
            Down(Interests, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostDeleteInterest(int order)
        {
            Delete(Interests, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostCopyInterest(int order)
        {
            if (Interests.Count < MaxInterests)
            {
                var copy = Interests[order].DeepClone();
                copy.Order = Interests.Count;
                Interests.Add(copy);
                Interests = CheckOrdering(Interests);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Interests);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadInterests(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadInterests(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Interest>(curriculumID);

            Interests = repository.GetInterests(curriculum, languageCode);
        }

        #endregion
    }
}