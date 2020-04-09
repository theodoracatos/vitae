using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Model.Poco;
using Model.ViewModels;
using Persistency.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vitae.Code;

namespace Vitae.Areas.Manage.Pages.Settings
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_SETTINGS = "_Settings";

        public int MaxCurriculumLanguages { get; } = 5;

        [BindProperty]
        public SettingVM Settings { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

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
                var curriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
                var copies = new List<CopyVM>(curriculumLanguages.Select(c => new CopyVM()));

                Settings = new SettingVM() { CurriculumLanguages = curriculumLanguages.ToList(), Copies = copies };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language)
                .Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID);
            var firstLanguageCode = existingCurriculumLanguages.Single(e => e.Order == 0).Language.LanguageCode;

            foreach (var curriculumLanguage in Settings.CurriculumLanguages)
            {
                if (!existingCurriculumLanguages.Any(cl => cl.Language.LanguageCode == curriculumLanguage.LanguageCode))
                {
                    var isFirstCurriculumLanguage = curriculumLanguage.Order == 0;
                    if (isFirstCurriculumLanguage)
                    {
                        // Remove 0
                        var curriculumLanguageToRemove = vitaeContext.CurriculumLanguages.Single(c => c.CurriculumID == curriculumID && c.Language.LanguageCode == firstLanguageCode);
                        vitaeContext.Curriculums.Include(c => c.CurriculumLanguages).Single(c => c.CurriculumID == curriculumID).CurriculumLanguages.Remove(curriculumLanguageToRemove);
                    }
                    // New
                    vitaeContext.Curriculums.Include(c => c.CurriculumLanguages).Single(c => c.CurriculumID == curriculumID).CurriculumLanguages.Add(
                        new CurriculumLanguage()
                        {
                            Curriculum = existingCurriculumLanguages.Single(e => e.Order == 0).Curriculum,
                            CurriculumID = existingCurriculumLanguages.Single(e => e.Order == 0).Curriculum.CurriculumID,
                            Language = vitaeContext.Languages.Single(l => l.LanguageCode == curriculumLanguage.LanguageCode),
                            LanguageID = vitaeContext.Languages.Single(l => l.LanguageCode == curriculumLanguage.LanguageCode).LanguageID,
                            Order = curriculumLanguage.Order
                        }
                    );
                    await vitaeContext.SaveChangesAsync();

                    if (isFirstCurriculumLanguage || Settings.Copies[curriculumLanguage.Order].Copy)
                    {
                        var languageCodeTo = vitaeContext.CurriculumLanguages.First(c => c.Order == 0).Language.LanguageCode;
                        await repository.CopyItemsFromCurriculumLanguage(curriculumID, firstLanguageCode, languageCodeTo, isFirstCurriculumLanguage);
                    }
                }
                else if (!Settings.CurriculumLanguages.Any(cl => cl.LanguageCode == curriculumLanguage.LanguageCode))
                {
                    // Remove
                    vitaeContext.CurriculumLanguages.Remove(vitaeContext.CurriculumLanguages.Single(cl => cl.Language.LanguageCode == curriculumLanguage.LanguageCode));
                    await repository.RemoveCurriculumLanguageItemsAsync(curriculumID, curriculumLanguage.LanguageCode);
                }
            }

            FillSelectionViewModel();

            return Page();
        }

        #endregion

        #region AJAX
        public IActionResult OnPostSelectChange()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }

        public IActionResult OnPostAddCurriculumLanguage()
        {
            if (Settings.CurriculumLanguages.Count < MaxCurriculumLanguages)
            {
                Settings.CurriculumLanguages.Add(new LanguageVM() { Order = Settings.CurriculumLanguages.Count });
                Settings.Copies.Add(new CopyVM() { Show = true });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }

        public IActionResult OnPostRemoveCurriculumLanguage()
        {
            Remove(Settings.CurriculumLanguages);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }
        #endregion


        #region HELPER
        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);

        }
        #endregion
    }
}
