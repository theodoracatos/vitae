using Library.Repository;
using Library.Resources;
using Model.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Education
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_EDUCATION = "_Education";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Educations), Prompt = nameof(SharedResource.Educations))]
        [BindProperty]
        public IList<EducationVM> Educations { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxEducations { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else if (vitaeContext.Curriculums.Include(c => c.Person).Single(c => c.Identifier == curriculumID).Person == null)
            {
                return BadRequest();
            }
            else
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                Educations = repository.GetEducations(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Educations);

                curriculum.Person.Educations =
                    Educations.Select(e => new Poco.Education()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Grade = e.Grade,
                        Order = e.Order,
                        Description = e.Description,
                        Link = e.Link,
                        SchoolName = e.SchoolName,
                        Subject = e.Subject,
                        Title = e.Title,
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == e.CountryCode)
                    }).ToList();

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

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostAddEducation()
        {
            if (Educations.Count == 0)
            {
                Educations.Add(new EducationVM() { Order = 1 });
            }
            else if (Educations.Count < MaxEducations)
            {
                Educations.Add(new EducationVM() { Order = Educations.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostRemoveEducation()
        {
            if (Educations.Count > 0)
            {
                Educations.RemoveAt(Educations.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostUpEducation(int order)
        {
            var education = Educations[order];
            Educations[order] = Educations[order - 1];
            Educations[order - 1] = education;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostDownEducation(int order)
        {
            var education = Educations[order];
            Educations[order] = Educations[order + 1];
            Educations[order + 1] = education;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
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