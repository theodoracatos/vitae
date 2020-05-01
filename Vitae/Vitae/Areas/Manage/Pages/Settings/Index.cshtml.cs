using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Model.Poco;
using Model.ViewModels;
using Persistency.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Vitae.Code;

namespace Vitae.Areas.Manage.Pages.Settings
{
    public class IndexModel : BasePageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IdentityContext identityContext;
        private readonly UserManager<IdentityUser> userManager;

        public const string PAGE_SETTINGS = "_Settings";

        public int MaxCurriculumLanguages { get; } = 4;

        [BindProperty]
        public SettingVM Setting { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public IndexModel(SignInManager<IdentityUser> signInManager, IStringLocalizer<SharedResource> localizer, IdentityContext identityContext, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.identityContext = identityContext;
        }

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

                Setting = new SettingVM()
                {
                    CurriculumLanguages = curriculumLanguages.ToList(),
                    SettingItems = curriculumLanguages.ToList().Select(cl =>
                        new SettingItemVM()
                        {
                            Copy = false,
                            FormerLanguageCode = cl.LanguageCode,
                            NrOfItems = (repository.CountItemsFromCurriculumLanguageAsync(curriculumID, cl.LanguageCode).Result).Sum(i => i.Value)
                        }).ToList()
                };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language).Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID);
            var firstLanguage = existingCurriculumLanguages.Single(e => e.Order == 0).Language;
            var deletedLanguageCodes = existingCurriculumLanguages.ToList().Select(e => e.Language.LanguageCode).Where(lc => !Setting.SettingItems.Any(s => s.FormerLanguageCode == lc));
            var ignoreOrder = new List<int>();

            // Delete
            await DeleteLanguagesAsync(existingCurriculumLanguages, deletedLanguageCodes);

            // Order
            for(int i = 0; i < vitaeContext.CurriculumLanguages.Count(cl => cl.CurriculumID == curriculumID); i++)
            {
                vitaeContext.CurriculumLanguages.Where(cl => cl.CurriculumID == curriculumID).OrderBy(c => c.Order).ToList()[i].Order = i;
            }
            await vitaeContext.SaveChangesAsync();

            existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language).Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID);

            // Change / Add
            for (int i = 0; i < Setting.CurriculumLanguages.Count; i++)
            {
                var existingLanguage = existingCurriculumLanguages.SingleOrDefault(e => e.Order == i)?.Language;
                var formerLanguageCode = Setting.SettingItems[i].FormerLanguageCode;
                var newLanguage = Setting.CurriculumLanguages[i];

                if(formerLanguageCode != null && formerLanguageCode != newLanguage.LanguageCode)
                {
                    var curriculum = existingCurriculumLanguages.Single(e => e.Order == i).Curriculum;

                    if (!vitaeContext.CurriculumLanguages.Any(c => c.Language.LanguageCode == newLanguage.LanguageCode))
                    {
                        // The language code has changed
                        vitaeContext.CurriculumLanguages.Remove(existingCurriculumLanguages.Single(o => o.Order == i));
                        AddCurriculumLanguage(curriculum, vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode), i);

                        // Change items
                        if (Setting.SettingItems[i].NrOfItems > 0)
                        {
                            await repository.MoveItemsFromCurriculumLanguageAsync(curriculumID, existingLanguage.LanguageCode, newLanguage.LanguageCode, copy: false);
                        }
                    }
                    else if(!ignoreOrder.Contains(i))
                    {
                        // Language switch occured
                        var newOrder = Setting.CurriculumLanguages.Single(c => c.LanguageCode == newLanguage.LanguageCode).Order;
                        var oldOrder = existingCurriculumLanguages.Single(c => c.Language.LanguageCode == newLanguage.LanguageCode).Order;
                        existingCurriculumLanguages.Single(c => c.Order == newOrder).Order = oldOrder;
                        existingCurriculumLanguages.Single(c => c.Language.LanguageCode == newLanguage.LanguageCode).Order = newOrder;

                        ignoreOrder.Add(oldOrder);

                    }
                    await vitaeContext.SaveChangesAsync();

                    // Update View Model
                    Setting.SettingItems[i].FormerLanguageCode = newLanguage.LanguageCode;
                    ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.FormerLanguageCode)}", new ValueProviderResult(newLanguage.LanguageCode, CultureInfo.InvariantCulture));

                }
                else if (existingLanguage == null)
                {
                    // There's a new langauge at the end
                    AddCurriculumLanguage(existingCurriculumLanguages.Single(e => e.Order == 0).Curriculum, vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode), i);
                    await vitaeContext.SaveChangesAsync();

                    if (Setting.SettingItems[i].Copy)
                    {
                        // Copy from first language
                        await repository.MoveItemsFromCurriculumLanguageAsync(curriculumID, firstLanguage.LanguageCode, newLanguage.LanguageCode, copy: true);
                    }

                    // Update View Model
                    var nrOfItems = (await repository.CountItemsFromCurriculumLanguageAsync(curriculumID, newLanguage.LanguageCode)).Sum(i => i.Value);
                    Setting.SettingItems[i].Copy = false;
                    Setting.SettingItems[i].FormerLanguageCode = newLanguage.LanguageCode;
                    Setting.SettingItems[i].NrOfItems = nrOfItems;
                    ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.Copy)}", new ValueProviderResult("false", CultureInfo.InvariantCulture));
                    ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.FormerLanguageCode)}", new ValueProviderResult(newLanguage.LanguageCode, CultureInfo.InvariantCulture));
                    ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.NrOfItems)}", new ValueProviderResult(nrOfItems.ToString(), CultureInfo.InvariantCulture));
                }

                // A change occured
                HasUnsafedChanges = false;
            }

            FillSelectionViewModel();

            return Page();
        }

        public async Task<RedirectResult> OnPostDeleteAccountAsync()
        {
            // Delete
            var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language).Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID).ToList();
            await DeleteLanguagesAsync(existingCurriculumLanguages, existingCurriculumLanguages.Select(e => e.Language.LanguageCode));

            vitaeContext.CurriculumLanguages.RemoveRange(vitaeContext.CurriculumLanguages.Include(c => c.Language).Where(c => c.CurriculumID == curriculumID));
            vitaeContext.Publications.RemoveRange(vitaeContext.Publications.Include(p => p.Curriculum).Where(p => p.Curriculum.CurriculumID == curriculumID));
            var curriculum = await repository.GetCurriculumAsync<PersonalDetail>(curriculumID);

            if (curriculum.PersonalDetails.Count == 1)
            {
                curriculum.PersonalDetails.Single().Children.ToList().ForEach(c => curriculum.PersonalDetails.Single().Children.Remove(c));
                curriculum.PersonalDetails.Single().PersonCountries.ToList().ForEach(p => curriculum.PersonalDetails.Single().PersonCountries.Remove(p));
                curriculum.PersonalDetails.Remove(curriculum.PersonalDetails.Single());
            }
            vitaeContext.Curriculums.Remove(vitaeContext.Curriculums.Single(c => c.CurriculumID == curriculumID));

            await vitaeContext.SaveChangesAsync();

            // Identity context
            var user = await userManager.GetUserAsync(User);
            identityContext.Users.Remove(identityContext.Users.Single(u => u.Id == user.Id));

            await identityContext.SaveChangesAsync();

            await signInManager.SignOutAsync();

            return Redirect("/");
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
            if (Setting.CurriculumLanguages.Count < MaxCurriculumLanguages)
            {
                Setting.CurriculumLanguages.Add(new LanguageVM() {  Order = Setting.CurriculumLanguages.Count });
                Setting.SettingItems.Add(new SettingItemVM());
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }

        public IActionResult OnPostDeleteCurriculumLanguage(int order)
        {
            Remove(Setting.CurriculumLanguages, order);
            Setting.SettingItems.Remove(Setting.SettingItems[order]);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }
        #endregion


        #region HELPER
        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task DeleteLanguagesAsync(IEnumerable<CurriculumLanguage> existingCurriculumLanguages, IEnumerable<string> deletedLanguageCodes)
        {
            // Delete
            foreach (var deletedLanguageCode in deletedLanguageCodes)
            {
                vitaeContext.CurriculumLanguages.Remove(existingCurriculumLanguages.Single(e => e.Language.LanguageCode == deletedLanguageCode));
                vitaeContext.Publications.RemoveRange(vitaeContext.Publications.Include(p => p.CurriculumLanguage).Where(p => p.Curriculum.CurriculumID == curriculumID && p.CurriculumLanguage.LanguageCode == deletedLanguageCode));
                
                await vitaeContext.SaveChangesAsync();

                await repository.DeleteItemsFromCurriculumLanguageAsync(curriculumID, deletedLanguageCode);
            }
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
