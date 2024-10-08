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

namespace Vitae.Areas.Manage.Pages.Languages
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_LANGUAGES = "_Languages";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Languages), Prompt = nameof(SharedResource.Languages))]
        [BindProperty]
        public IList<LanguageSkillVM> LanguageSkills { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public int MaxLanguageSkills { get; } = 10;

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

                await LoadLanguageSkillsAsync();

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<LanguageSkill>(curriculumID);
                vitaeContext.RemoveRange(curriculum.LanguageSkills.Where(e => e.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                LanguageSkills.Select(l => new Poco.LanguageSkill()
                {
                    Order = l.Order,
                    Rate = l.Rate,
                    SpokenLanguage = vitaeContext.Languages.Single(la => la.LanguageCode == l.LanguageCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(l => curriculum.LanguageSkills.Add(l));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostSelectChange()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostAddLanguageSkill()
        {
            if (LanguageSkills.Count < MaxLanguageSkills)
            {
                LanguageSkills.Add(new LanguageSkillVM() 
                { 
                    Order = LanguageSkills.Count,
                    Collapsed = base.Collapsed
                });
                LanguageSkills = CheckOrdering(LanguageSkills);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostRemoveLanguageSkill()
        {
            Remove(LanguageSkills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostUpLanguageSkill(int order)
        {
            Up(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDownLanguageSkill(int order)
        {
            Down(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDeleteLanguageSkill(int order)
        {
            Delete(LanguageSkills, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostCopyLanguageSkill(int order)
        {
            if (LanguageSkills.Count < MaxLanguageSkills)
            {
                var copy = LanguageSkills[order].DeepClone();
                copy.Order = LanguageSkills.Count;
                LanguageSkills.Add(copy);
                copy.LanguageCode = string.Empty;
                LanguageSkills = CheckOrdering(LanguageSkills);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(LanguageSkills);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadLanguageSkillsAsync();
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadLanguageSkillsAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<LanguageSkill>(curriculumID);

            LanguageSkills = repository.GetLanguageSkills(curriculum, CurriculumLanguageCode);
        }

        #endregion
    }
}