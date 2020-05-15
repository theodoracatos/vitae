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

namespace Vitae.Areas.Manage.Pages.Courses
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_COURSES = "_Courses";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Courses), Prompt = nameof(SharedResource.Courses))]
        [BindProperty]
        public IList<CourseVM> Courses { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxCourses { get; } = 40;

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
                var curriculum = await repository.GetCurriculumAsync<Course>(curriculumID);
                LoadLanguageCode(curriculum);

                await LoadCourses(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Course>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Courses.Where(c => c.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Courses.Select(c => new Poco.Course()
                {
                    City = c.City,
                    Start = new DateTime(c.Start_Year, c.Start_Month, c.Start_Day),
                    End = c.SingleDay ? null : (DateTime?)new DateTime(c.End_Year.Value, c.End_Month.Value, c.End_Day.Value),
                    Order = c.Order,
                    Description = c.Description,
                    Link = c.Link,
                    SchoolName = c.SchoolName,
                    Title = c.Title,
                    Country = vitaeContext.Countries.Single(cc => cc.CountryCode == c.CountryCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(c => curriculum.Courses.Add(c));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeSingleDay()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostAddCourse()
        {
            if (Courses.Count < MaxCourses)
            {
                Courses.Add(new CourseVM() {
                    Order = Courses.Count,
                    Start_Day = DateTime.Now.Day,
                    Start_Month = DateTime.Now.Month,
                    Start_Year = DateTime.Now.Year,
                    End_Day = DateTime.Now.Day,
                    End_Month = DateTime.Now.Month,
                    End_Year = DateTime.Now.Year,
                    Collapsed = base.Collapsed
                });
                Courses = CheckOrdering(Courses);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostRemoveCourse()
        {
            Remove(Courses);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostUpCourse(int order)
        {
            Up(Courses, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostDownCourse(int order)
        {
            Down(Courses, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostDeleteCourse(int order)
        {
            Delete(Courses, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostCopyCourse(int order)
        {
            if (Courses.Count < MaxCourses)
            {
                var copy = Courses[order].DeepClone();
                copy.Order = Courses.Count;
                Courses.Add(copy);
                Courses = CheckOrdering(Courses);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostChangeDate(int order)
        {
            Courses[order].Start_Day = CorrectDate(Courses[order].Start_Year, Courses[order].Start_Month, Courses[order].Start_Day);
            if (!Courses[order].SingleDay)
            {
                Courses[order].End_Day = CorrectDate(Courses[order].End_Year.Value, Courses[order].End_Month.Value, Courses[order].End_Day.Value);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Courses);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadCourses(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_COURSES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadCourses(string languageCode, Curriculum curr = null)
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            var curriculum = curr ?? await repository.GetCurriculumAsync<Course>(curriculumID);

            Courses = repository.GetCourses(curriculum, languageCode);
        }
        #endregion
    }
}