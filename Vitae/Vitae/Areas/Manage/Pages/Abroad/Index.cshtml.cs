using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

namespace Vitae.Areas.Manage.Pages.Abroad
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_ABROAD = "_Abroad";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Abroad), Prompt = nameof(SharedResource.Abroad))]
        [BindProperty]
        public IList<AbroadVM> Abroads { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxAbroads { get; } = 10;

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
                Abroads = repository.GetAbroads(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Abroads);

                curriculum.Person.Abroads =
                    Abroads.Select(e => new Poco.Abroad()
                    { 
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, DateTime.DaysInMonth(e.End_Year.Value, e.End_Month.Value)),
                        Order = e.Order,
                        Description = e.Description,
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
            Abroads[order].UntilNow = !Abroads[order].UntilNow;
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROAD);
        }

        public IActionResult OnPostAddAbroad()
        {
            if (Abroads.Count < MaxAbroads)
            {
                Abroads.Add(new AbroadVM() 
                {
                    Order = Abroads.Count,
                    Start_Month = DateTime.Now.Month,
                    Start_Year = DateTime.Now.Year,
                    End_Month = DateTime.Now.Month,
                    End_Year = DateTime.Now.Year
                });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROAD);
        }

        public IActionResult OnPostRemoveAbroad()
        {
            if (Abroads.Count > 0)
            {
                Abroads.RemoveAt(Abroads.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROAD);
        }

        public IActionResult OnPostUpAbroad(int order)
        {
            var abroad = Abroads[order];
            Abroads[order] = Abroads[order - 1];
            Abroads[order - 1] = abroad;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROAD);
        }

        public IActionResult OnPostDownAbroad(int order)
        {
            var abroad = Abroads[order];
            Abroads[order] = Abroads[order + 1];
            Abroads[order + 1] = abroad;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROAD);
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