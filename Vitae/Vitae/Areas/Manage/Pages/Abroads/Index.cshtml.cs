﻿using Library.Extensions;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

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

        public int MaxAbroads { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
    : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                LoadLanguageCode();
                var curriculum = await repository.GetCurriculumAsync<Abroad>(curriculumID);

                await LoadAbroadsAsync();
                FillSelectionViewModel();

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Abroad>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Abroads.Where(c => c.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Abroads.Select(e => new Poco.Abroad()
                {
                    City = e.City,
                    Start = new DateTime(e.Start_Year, e.Start_Month, 1),
                    End = e.UntilNow ? null : (DateTime?)new DateTime(e.End_Year.Value, e.End_Month.Value, 1),
                    Order = e.Order,
                    Description = e.Description,
                    Country = vitaeContext.Countries.Single(c => c.CountryCode == e.CountryCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(a => curriculum.Abroads.Add(a));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeUntilNow()
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
                    End_Year = DateTime.Now.Year,
                    Collapsed = base.Collapsed
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

        public IActionResult OnPostCopyAbroad(int order)
        {
            if (Abroads.Count < MaxAbroads)
            {
                var copy = Abroads[order].DeepClone();
                copy.Order = Abroads.Count;
                Abroads.Add(copy);
                Abroads = CheckOrdering(Abroads);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Abroads);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadAbroadsAsync();
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABROADS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadAbroadsAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<Abroad>(curriculumID);

            Abroads = repository.GetAbroads(curriculum, CurriculumLanguageCode);
        }
        #endregion
    }
}