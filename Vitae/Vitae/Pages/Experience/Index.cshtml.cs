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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

namespace Vitae.Pages.Experience
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_EXPERIENCE = "_Experience";
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.JobExperiences), Prompt = nameof(SharedResource.JobExperiences))]
        [BindProperty]
        public IList<ExperienceVM> Experiences { get; set; }

        public int MaxExperiences { get; } = 20;

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

                Experiences = curriculum.Person.Experiences?.OrderBy(ex => ex.Order)
                    .Select(e => new ExperienceVM()
                    {
                        City = e.City,
                        Start_Month = e.Start.Month,
                        Start_Year = e.Start.Year,
                        End_Month = e.End.HasValue ? e.End.Value.Month : DateTime.Now.Month,
                        End_Year = e.End.HasValue ? e.End.Value.Year : DateTime.Now.Year,
                        UntilNow = !e.End.HasValue,
                        Order = e.Order,
                        Resumee = e.Resumee,
                        CompanyLink = e.CompanyLink,
                        CompanyName = e.CompanyName,
                        JobTitle = e.JobTitle
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
                appContext.RemoveRange(curriculum.Person.Experiences);

                curriculum.Person.Experiences =
                    Experiences.Select(e => new Poco.Experience()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Order = e.Order,
                        Resumee = e.Resumee,
                        CompanyLink = e.CompanyLink,
                        CompanyName = e.CompanyName,
                        JobTitle = e.JobTitle
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

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }

        public IActionResult OnPostAddExperience()
        {
            if (Experiences.Count == 0)
            {
                Experiences.Add(new ExperienceVM() { Order = 1 });
            }
            else if (Experiences.Count < MaxExperiences)
            {
                Experiences.Add(new ExperienceVM() { Order = Experiences.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }

        public IActionResult OnPostRemoveExperience()
        {
            if (Experiences.Count > 0)
            {
                Experiences.RemoveAt(Experiences.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }

        public IActionResult OnPostUpExperience(int order)
        {
            var experience = Experiences[order];
            Experiences[order] = Experiences[order - 1];
            Experiences[order - 1] = experience;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }

        public IActionResult OnPostDownExperience(int order)
        {
            var experience = Experiences[order];
            Experiences[order] = Experiences[order + 1];
            Experiences[order + 1] = experience;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }

        #endregion

        #region Helper

        private Curriculum GetCurriculum()
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Experiences)
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