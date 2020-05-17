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

namespace Vitae.Areas.Manage.Pages.Educations
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_EDUCATIONS = "_Educations";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Educations), Prompt = nameof(SharedResource.Educations))]
        [BindProperty]
        public IList<EducationVM> Educations { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxEducations { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

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
                var curriculum = await repository.GetCurriculumAsync<Education>(curriculumID);
                LoadLanguageCode(curriculum);

                await LoadEducations(CurriculumLanguageCode, curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Education>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Educations.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Educations.Select(e => new Poco.Education()
                {
                    City = e.City,
                    Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                    End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, 1),
                    Grade = e.Grade,
                    Order = e.Order,
                    Description = e.Description,
                    Link = e.Link,
                    SchoolName = e.SchoolName,
                    Subject = e.Subject,
                    Title = e.Title,
                    Country = vitaeContext.Countries.Single(c => c.CountryCode == e.CountryCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(e => curriculum.Educations.Add(e));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            
            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostChangeUntilNow()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostAddEducation()
        {
            if (Educations.Count < MaxEducations)
            {
                Educations.Add(new EducationVM() {
                    Order = Educations.Count,
                    Start_Month = DateTime.Now.Month,
                    Start_Year = DateTime.Now.Year,
                    End_Month = DateTime.Now.Month,
                    End_Year = DateTime.Now.Year,
                    Collapsed = base.Collapsed
                });
                Educations = CheckOrdering(Educations);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostRemoveEducation()
        {
            Remove(Educations);
            
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostUpEducation(int order)
        {
            Up(Educations, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostDownEducation(int order)
        {
            Down(Educations, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostDeleteEducation(int order)
        {
            Delete(Educations, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostCopyEducation(int order)
        {
            if (Educations.Count < MaxEducations)
            {
                var copy = Educations[order].DeepClone();
                copy.Order = Educations.Count;
                Educations.Add(copy);
                Educations = CheckOrdering(Educations);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Educations);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadEducations(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadEducations(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Education>(curriculumID);

            Educations = repository.GetEducations(curriculum, languageCode);
        }
        #endregion
    }
}