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

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Languages
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_LANGUAGES = "_Languages";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Languages), Prompt = nameof(SharedResource.Languages))]
        [BindProperty]
        public IList<LanguageSkillVM> LanguageSkills { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public int MaxLanguageSkills { get; } = 10;

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
                var curriculum = await repository.GetCurriculumAsync<LanguageSkill>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadLanguageSkills(CurriculumLanguageCode, curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<LanguageSkill>(curriculumID);
                vitaeContext.RemoveRange(curriculum.LanguageSkills.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                LanguageSkills.Select(l => new Poco.LanguageSkill()
                {
                    Order = l.Order,
                    Rate = l.Rate,
                    SpokenLanguage = vitaeContext.Languages.Single(la => la.LanguageCode == l.LanguageCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(l => curriculum.LanguageSkills.Add(l));
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

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostAddLanguageSkill()
        {
            if (LanguageSkills.Count < MaxLanguageSkills)
            {
                LanguageSkills.Add(new LanguageSkillVM() 
                { 
                    Order = LanguageSkills.Count,
                    Collapsed = base.Collapsed
                });
                LanguageSkills = CheckOrdering(LanguageSkills);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostRemoveLanguageSkill()
        {
            Remove(LanguageSkills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostUpLanguageSkill(int order)
        {
            Up(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDownLanguageSkill(int order)
        {
            Down(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDeleteLanguageSkill(int order)
        {
            Delete(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostCopyLanguageSkill(int order)
        {
            if (LanguageSkills.Count < MaxLanguageSkills)
            {
                var copy = LanguageSkills[order].DeepClone();
                copy.Order = LanguageSkills.Count;
                LanguageSkills.Add(copy);
                copy.LanguageCode = string.Empty;
                LanguageSkills = CheckOrdering(LanguageSkills);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(LanguageSkills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadLanguageSkills(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadLanguageSkills(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<LanguageSkill>(curriculumID);

            LanguageSkills = repository.GetLanguageSkills(curriculum, languageCode);
        }

        #endregion
    }
}