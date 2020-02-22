using Library.Repository;
using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

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
                var curriculum = GetCurriculum();

                Educations = curriculum.Person.Educations?.OrderBy(ed => ed.Order)
                    .Select(e => new EducationVM() {
                        City = e.City,
                        Start_Month = e.Start.Month,
                        Start_Year = e.Start.Year,
                        End_Month = e.End.HasValue ? e.End.Value.Month : DateTime.Now.Month,
                        End_Year = e.End.HasValue ? e.End.Value.Year : DateTime.Now.Year,
                        UntilNow = !e.End.HasValue,
                        Grade = e.Grade,
                        Order = e.Order,
                        Resumee = e.Resumee,
                        Link = e.Link,
                        SchoolName = e.SchoolName,
                        Subject = e.Subject,
                        Title = e.Title,
                        CountryCode = e.Country.CountryCode
                    })
                    .ToList();

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = GetCurriculum();
                vitaeContext.RemoveRange(curriculum.Person.Educations);

                curriculum.Person.Educations =
                    Educations.Select(e => new Poco.Education()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Grade = e.Grade,
                        Order = e.Order,
                        Resumee = e.Resumee,
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
        public IActionResult OnPostChangeCountry()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }
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

        private Curriculum GetCurriculum()
        {
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Educations).ThenInclude(e => e.Country)
                    .Single(c => c.Identifier == curriculumID);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Months = vitaeContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);

            Countries = vitaeContext.Countries.Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
            requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
            requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
            requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
            c.Name,
                PhoneCode = c.PhoneCode
            }).OrderBy(c => c.Name);
        }
        #endregion
    }
}