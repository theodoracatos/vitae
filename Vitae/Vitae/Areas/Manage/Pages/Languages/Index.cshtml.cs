using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                LanguageSkills = repository.GetLanguageSkills(curriculum);
                
                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.LanguageSkills);

                curriculum.Person.LanguageSkills =
                    LanguageSkills.Select(l => new Poco.LanguageSkill()
                    {
                        Order = l.Order,
                        Rate = l.Rate,
                        Language = vitaeContext.Languages.Single(la => la.LanguageCode == l.LanguageCode)
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

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostAddLanguageSkill()
        {
            if (LanguageSkills.Count < MaxLanguageSkills)
            {
                LanguageSkills.Add(new LanguageSkillVM() { Order = LanguageSkills.Count });
            }
            LanguageSkills = CheckOrdering(LanguageSkills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostRemoveLanguageSkill()
        {
            if (LanguageSkills.Count > 0)
            {
                LanguageSkills.RemoveAt(LanguageSkills.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostUpLanguageSkill(int order)
        {
            var languageSkill = LanguageSkills[order];
            LanguageSkills[order] = LanguageSkills[order - 1];
            LanguageSkills[order - 1] = languageSkill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDownLanguageSkill(int order)
        {
            var languageSkill = LanguageSkills[order];
            LanguageSkills[order] = LanguageSkills[order + 1];
            LanguageSkills[order + 1] = languageSkill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDeleteLanguageSkill(int order)
        {
            LanguageSkills.Remove(LanguageSkills.Single(e => e.Order == order));

            for (int i = 0; i < LanguageSkills.Count; i++)
            {
                LanguageSkills[i].Order = i;
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}