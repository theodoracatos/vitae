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

namespace Vitae.Areas.Manage.Pages.Skills
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_SKILLS = "_Skills";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Skills), Prompt = nameof(SharedResource.Skills))]
        [BindProperty]
        public IList<SkillVM> Skills { get; set; }

        public int MaxSkills { get; } = 10;

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
                var curriculum = await repository.GetCurriculumAsync<Skill>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadSkills(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Skill>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Skills.Where(s => s.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Skills.Select(s => new Poco.Skill()
                    {
                        Category = s.Category,
                        Order = s.Order,
                        Skillset = s.Skillset,
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(s => curriculum.Skills.Add(s));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostAddSkill()
        {
            if (Skills.Count < MaxSkills)
            {
                Skills.Add(new SkillVM() 
                { 
                    Order = Skills.Count,
                    Collapsed = base.Collapsed
                });
                Skills = CheckOrdering(Skills);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostRemoveSkill()
        {
            Remove(Skills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostUpSkill(int order)
        {
            Up(Skills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostDownSkill(int order)
        {
            Down(Skills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public async Task<IActionResult> OnPostDeleteSkillAsync(int order)
        {
            Delete(Skills, order);

            var curriculum = await repository.GetCurriculumAsync<Skill>(curriculumID);

            var item = curriculum.Skills.SingleOrDefault(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order == order);
            if (item != null)
            {
                vitaeContext.Remove(item);
                curriculum.Skills.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order > order).ToList().ForEach(e => e.Order = e.Order - 1);

                await vitaeContext.SaveChangesAsync();
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostCopySkill(int order)
        {
            if (Skills.Count < MaxSkills)
            {
                var copy = Skills[order].DeepClone();
                copy.Order = Skills.Count;
                Skills.Add(copy);
                Skills = CheckOrdering(Skills);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Skills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadSkills(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadSkills(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Skill>(curriculumID);

            Skills = repository.GetSkills(curriculum, languageCode);
        }

        #endregion
    }
}