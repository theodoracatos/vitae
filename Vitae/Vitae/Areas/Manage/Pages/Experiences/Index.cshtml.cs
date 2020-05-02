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

namespace Vitae.Areas.Manage.Pages.Experiences
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_EXPERIENCES = "_Experiences";

        public int MaxExperiences { get; } = 20;

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.JobExperiences), Prompt = nameof(SharedResource.JobExperiences))]
        [BindProperty]
        public IList<ExperienceVM> Experiences { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public IEnumerable<IndustryVM> Industries { get; set; }

        public IEnumerable<HierarchyLevelVM> HierarchyLevels{ get; set; }

        public IEnumerable<MonthVM> Months { get; set; }

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
                var curriculum = await repository.GetCurriculumAsync<Experience>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadExperiences(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Experience>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Experiences.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Experiences.Select(e => new Poco.Experience()
                {
                    City = e.City,
                    Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                    End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, 1),
                    Order = e.Order,
                    Description = e.Description,
                    Link = e.Link,
                    CompanyName = e.CompanyName,
                    JobTitle = e.JobTitle,
                    Country = vitaeContext.Countries.Single(c => c.CountryCode == e.CountryCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode),
                    HierarchyLevel = vitaeContext.HierarchyLevels.Single(h => h.HierarchyLevelCode == e.HierarchyLevelCode),
                    Industry = vitaeContext.Industries.Single(i => i.IndustryCode == e.IndustryCode),
                }).ToList().ForEach(e => curriculum.Experiences.Add(e));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeUntilNow(int order)
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostAddExperience()
        {
            if (Experiences.Count < MaxExperiences)
            {
                Experiences.Add(new ExperienceVM() 
                {
                    Order = Experiences.Count, 
                    Start_Month = DateTime.Now.Month, 
                    Start_Year = DateTime.Now.Year, 
                    End_Month = DateTime.Now.Month, 
                    End_Year = DateTime.Now.Year,
                    Collapsed = base.Collapsed
                });
                Experiences = CheckOrdering(Experiences);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostRemoveExperience()
        {
            Remove(Experiences);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostUpExperience(int order)
        {
            Up(Experiences, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostDownExperience(int order)
        {
            Down(Experiences, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public async Task<IActionResult> OnPostDeleteExperienceAsync(int order)
        {
            Delete(Experiences, order);

            var curriculum = await repository.GetCurriculumAsync<Experience>(curriculumID);

            var item = curriculum.Experiences.SingleOrDefault(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order == order);
            if (item != null)
            {
                vitaeContext.Remove(item);
                curriculum.Experiences.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode && e.Order > order).ToList().ForEach(e => e.Order = e.Order - 1);

                await vitaeContext.SaveChangesAsync();
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostCopyExperience(int order)
        {
            if (Experiences.Count < MaxExperiences)
            {
                var copy = Experiences[order].DeepClone();
                copy.Order = Experiences.Count;
                Experiences.Add(copy);
                Experiences = CheckOrdering(Experiences);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Experiences);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadExperiences(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Industries = repository.GetIndustries(requestCulture.RequestCulture.UICulture.Name);
            HierarchyLevels = repository.GetHierarchyLevels(requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadExperiences(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Experience>(curriculumID);

            Experiences = repository.GetExperiences(curriculum, languageCode);
        }
        #endregion
    }
}