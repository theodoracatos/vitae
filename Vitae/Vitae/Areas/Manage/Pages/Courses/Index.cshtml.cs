﻿using Library.Extensions;
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
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                Courses = repository.GetCourses(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Courses);

                curriculum.Person.Courses =
                    Courses.Select(e => new Poco.Course()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.SingleDay ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, e.Start_Day),
                        Order = e.Order,
                        Description = e.Description,
                        Link = e.Link,
                        SchoolName = e.SchoolName,
                        Title = e.Title,
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == e.CountryCode)
                    }).ToList();
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeSingleDay(int order)
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

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
        }
        #endregion
    }
}