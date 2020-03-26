﻿using Library.Repository;
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
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                Educations = repository.GetEducations(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Educations);

                curriculum.Person.Educations =
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

        public IActionResult OnPostChangeUntilNow(int order)
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
                    End_Year = DateTime.Now.Year
                });
                Educations = CheckOrdering(Educations);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostRemoveEducation()
        {
            if (Educations.Count > 0)
            {
                Educations.RemoveAt(Educations.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostUpEducation(int order)
        {
            var education = Educations[order];
            Educations[order] = Educations[order - 1];
            Educations[order - 1] = education;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostDownEducation(int order)
        {
            var education = Educations[order];
            Educations[order] = Educations[order + 1];
            Educations[order + 1] = education;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
        }

        public IActionResult OnPostDeleteEducation(int order)
        {
            Educations.Remove(Educations.Single(e => e.Order == order));

            for (int i = 0; i < Educations.Count; i++)
            {
                Educations[i].Order = i;
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_EDUCATIONS);
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