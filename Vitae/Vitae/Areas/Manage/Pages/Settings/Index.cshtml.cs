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
                var copies = new List<bool>(curriculumLanguages.Select(c => false));
                var nrOfItems = curriculumLanguages.Select(cl => repository.CountItemsFromCurriculumLanguageAsync(curriculumID, cl.LanguageCode).Result).ToList();

                Settings = new SettingVM() 
                {
                    CurriculumLanguages = curriculumLanguages.ToList(),
                    FormerLanguageCodes = curriculumLanguages.Select(cl => cl.LanguageCode).ToList(),
                    Copies = copies,
                    NrOfItems = nrOfItems
                };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language)
                .Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID);
            var firstLanguage = existingCurriculumLanguages.Single(e => e.Order == 0).Language;
            var deletedLanguageCodes = existingCurriculumLanguages.ToList().Select(e => e.Language.LanguageCode).Where(lc => !Settings.FormerLanguageCodes.Any(fc => fc == lc));

            // Delete
            foreach (var deletedLanguageCode in deletedLanguageCodes)
            {
                vitaeContext.CurriculumLanguages.Remove(existingCurriculumLanguages.Single(e => e.Language.LanguageCode == deletedLanguageCode));
                await vitaeContext.SaveChangesAsync();

                await repository.DeleteItemsFromCurriculumLanguageAsync(curriculumID, deletedLanguageCode);
            }

            // Order
            for(int i = 0; i < vitaeContext.CurriculumLanguages.Count(cl => cl.CurriculumID == curriculumID); i++)
            {
                vitaeContext.CurriculumLanguages.Where(cl => cl.CurriculumID == curriculumID).ToList()[i].Order = i;
            }
            await vitaeContext.SaveChangesAsync();

            // Change / Add
            for (int i = 0; i < Settings.CurriculumLanguages.Count; i++)
            {
                var existingLanguage = existingCurriculumLanguages.SingleOrDefault(e => e.Order == i)?.Language;
                var formerLanguageCode = Settings.FormerLanguageCodes[i];
                var newLanguage = Settings.CurriculumLanguages[i];

                if(formerLanguageCode != null && formerLanguageCode != newLanguage.LanguageCode)
                {
                    // The language code has changed
                    var curriculum = existingCurriculumLanguages.Single(e => e.Order == i).Curriculum;
                    vitaeContext.CurriculumLanguages.Remove(existingCurriculumLanguages.Single(o => o.Order == i));
                    AddCurriculumLanguage(curriculum, vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode), i);
                    await vitaeContext.SaveChangesAsync();

                    // Change items
                    if (Settings.NrOfItems[i] > 0)
                    {
                        await repository.MoveItemsFromCurriculumLanguageAsync(curriculumID, existingLanguage.LanguageCode, newLanguage.LanguageCode, copy: false);
                    }
                }
                else if (existingLanguage == null)
                {
                    // There's a new langauge at the end
                    AddCurriculumLanguage(existingCurriculumLanguages.Single(e => e.Order == 0).Curriculum, vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode), i);
                    await vitaeContext.SaveChangesAsync();

                    if (Settings.Copies[i])
                    {
                        // Copy from first language
                        await repository.MoveItemsFromCurriculumLanguageAsync(curriculumID, firstLanguage.LanguageCode, newLanguage.LanguageCode, copy: true);
                    }
                }

                Settings.Copies[i] = false;
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
                Settings.CurriculumLanguages.Add(new LanguageVM() {  Order = Settings.CurriculumLanguages.Count });
                if (Settings.Copies.Count < Settings.CurriculumLanguages.Count)
                {
                    Settings.Copies.Add(true);
                }
                else
                {
                    Settings.Copies[Settings.Copies.Count - 1] = true;
                }
                Settings.FormerLanguageCodes.Add("");
                Settings.NrOfItems.Add(0);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }

        public IActionResult OnPostDeleteCurriculumLanguage(int order)
        {
            Remove(Settings.CurriculumLanguages, order);
            Settings.FormerLanguageCodes.RemoveAt(order);
            Settings.NrOfItems.RemoveAt(order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }
        #endregion


        #region HELPER
        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        private void AddCurriculumLanguage(Curriculum curriculum, Language newLanguage, int order)
        {
            vitaeContext.Curriculums.Include(c => c.CurriculumLanguages).Single(c => c.CurriculumID == curriculumID).CurriculumLanguages.Add(
                        new CurriculumLanguage()
                        {
                            Curriculum = curriculum,
                            CurriculumID = curriculum.CurriculumID,
                            Language = vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode),
                            LanguageID = vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode).LanguageID,
                            Order = order
                        }
                    );
        }
        #endregion
    }
}
