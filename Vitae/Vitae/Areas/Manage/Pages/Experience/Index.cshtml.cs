﻿using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

namespace Vitae.Areas.Manage.Pages.Experience
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_EXPERIENCE = "_Experience";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.JobExperiences), Prompt = nameof(SharedResource.JobExperiences))]
        [BindProperty]
        public IList<ExperienceVM> Experiences { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxExperiences { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
            : base(localizer, vitaeContext, httpContextAccessor, userManager) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
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
                        Link = e.Link,
                        CompanyName = e.CompanyName,
                        JobTitle = e.JobTitle,
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
                vitaeContext.RemoveRange(curriculum.Person.Experiences);

                curriculum.Person.Experiences =
                    Experiences.Select(e => new Poco.Experience()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Order = e.Order,
                        Resumee = e.Resumee,
                        Link = e.Link,
                        CompanyName = e.CompanyName,
                        JobTitle = e.JobTitle,
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

            return GetPartialViewResult(PAGE_EXPERIENCE);
        }
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
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Experiences).ThenInclude(e => e.Country)
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