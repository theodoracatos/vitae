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
                Experiences = repository.GetExperiences(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Experiences);

                curriculum.Person.Experiences =
                    Experiences.Select(e => new Poco.Experience()
                    {
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Order = e.Order,
                        Description = e.Description,
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

        protected override void FillSelectionViewModel()
        {
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
        }
        #endregion
    }
}