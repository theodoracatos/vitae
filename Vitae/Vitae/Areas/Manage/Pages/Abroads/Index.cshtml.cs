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

namespace Vitae.Areas.Manage.Pages.Abroads
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_ABROADS = "_Abroads";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Abroads), Prompt = nameof(SharedResource.Abroads))]
        [BindProperty]
        public IList<AbroadVM> Abroads { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxAbroads { get; } = 10;

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
                Abroads = repository.GetAbroads(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Abroads);

                curriculum.Person.Abroads =
                    Abroads.Select(e => new Poco.Abroad()
                    { 
                        City = e.City,
                        Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                        End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, 1),
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
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
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
                Abroads = CheckOrdering(Abroads);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public IActionResult OnPostRemoveAbroad()
        {
            Remove(Abroads);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public IActionResult OnPostUpAbroad(int order)
        {
            Up(Abroads, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public IActionResult OnPostDownAbroad(int order)
        {
            Down(Abroads, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public IActionResult OnPostDeleteAbroad(int order)
        {
            Delete(Abroads, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
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