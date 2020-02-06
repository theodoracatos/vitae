using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

namespace Vitae.Pages.Education
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_EDUCATION = "_Education";
        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [BindProperty]
        public List<EducationVM> Educations { get; set; }

        public int MaxEducations { get; } = 20;
        public int YearStart { get; } = 1900;

        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        public IEnumerable<MonthVM> Months { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
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
                        SchoolLink = e.SchoolLink,
                        SchoolName = e.SchoolName,
                        Subject = e.Subject,
                        Title = e.Title
                    })
                    .ToList() ?? new List<EducationVM>() { new EducationVM() { Order = 1 } };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = GetCurriculum();
                appContext.RemoveRange(curriculum.Person.Educations);

                curriculum.Person.Educations =
                    Educations.Select(e => new Poco.Education()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Grade = e.Grade,
                        Order = e.Order,
                        Resumee = e.Resumee,
                        SchoolLink = e.SchoolLink,
                        SchoolName = e.SchoolName,
                        Subject = e.Subject,
                        Title = e.Title
                    }).ToList();

                await appContext.SaveChangesAsync();
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
            if (Educations == null)
            {
                Educations = new List<EducationVM>() { new EducationVM() { Order = 0 } };
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
            if (Educations == null)
            {
                Educations = new List<EducationVM>() { new EducationVM() { Order = 0} };
            }
            else if (Educations.Count > 1)
            {
                Educations.RemoveAt(Educations.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATION);
        }

        public IActionResult OnPostUpEducation(int order)
        {
            // TODO - + now() date end
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
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Educations)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Months = appContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);
        }
        #endregion
    }
}