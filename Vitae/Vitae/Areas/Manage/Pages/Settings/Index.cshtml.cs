using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Manage.Pages.Settings
{
    public class IndexModel : BasePageModel
    {
        protected readonly IdentityContext identityContext;

        public const string PAGE_SETTINGS = "_Settings";

        public int MaxCurriculumLanguages { get; } = 5;

        [BindProperty]
        public SettingVM Setting { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository, SignInManager<IdentityUser> signInManager, IEmailSender emailSender, IdentityContext identityContext)
        : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender) 
        {
            this.identityContext = identityContext;
        }
        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                var curriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
                var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);
                var countPerLanguages = await repository.CountItemsFromCurriculumAsync(curriculumID);
                var publications = repository.GetPublications(curriculum, this.BaseUrl);

                Setting = new SettingVM()
                {
                    CurriculumLanguages = curriculumLanguages.ToList(),
                    SettingItems = curriculumLanguages.ToList().Select(cl =>
                        new SettingItemVM()
                        {
                            Copy = false,
                            FormerLanguageCode = cl.LanguageCode,
                            NrOfItems = countPerLanguages[cl.LanguageCode].Sum(i => i.Value),
                            HasPublication = publications.Any(p => p.LanguageCode == cl.LanguageCode)
                        }).ToList()
                };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language).Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID).OrderBy(c => c.Order);
                var firstLanguage = existingCurriculumLanguages.Single(e => e.Order == 0).Language;
                var deletedLanguageCodes = existingCurriculumLanguages.ToList().Select(e => e.Language.LanguageCode).Where(lc => !Setting.SettingItems.Any(s => s.FormerLanguageCode == lc));
                var ignoreOrder = new List<int>();

                // Delete
                foreach (var deletedLanguageCode in deletedLanguageCodes)
                {
                    vitaeContext.Entry(existingCurriculumLanguages.Single(e => e.Language.LanguageCode == deletedLanguageCode)).State = EntityState.Deleted;

                    await vitaeContext.SaveChangesAsync();
                    await repository.DeleteItemsFromCurriculumLanguageAsync(curriculumID, deletedLanguageCode);
                }

                // Order if gaps available
                if (deletedLanguageCodes.Count() > 0)
                {
                    var order = 0;
                    foreach (var curriculumLanguage in existingCurriculumLanguages)
                    {
                        curriculumLanguage.Order = order++;
                    }
                    await vitaeContext.SaveChangesAsync();
                }

                // Change / Add
                for (int i = 0; i < Setting.CurriculumLanguages.Count; i++)
                {
                    var existingLanguage = existingCurriculumLanguages.SingleOrDefault(e => e.Order == i)?.Language;
                    var formerLanguageCode = Setting.SettingItems[i].FormerLanguageCode;
                    var newLanguage = Setting.CurriculumLanguages[i];

                    if (formerLanguageCode != null && formerLanguageCode != newLanguage.LanguageCode)
                    {
                        var curriculum = existingCurriculumLanguages.Single(e => e.Order == i).Curriculum;

                        if (!existingCurriculumLanguages.Any(c => c.Language.LanguageCode == newLanguage.LanguageCode))
                        {
                            // The language code has changed
                            vitaeContext.Entry(existingCurriculumLanguages.Single(o => o.Order == i)).State = EntityState.Deleted;
                            AddCurriculumLanguage(curriculum, vitaeContext.Languages.Single(l => l.LanguageCode == newLanguage.LanguageCode), i);

                            // Change language
                            await repository.MoveItemsFromCurriculumLanguageAsync(curriculumID, existingLanguage.LanguageCode, newLanguage.LanguageCode, copy: false);
                        }
                        else if (!ignoreOrder.Contains(i))
                        {
                            if(i == 0)
                            {
                                // Language switch occured (1 <=> 2)
                                await repository.MoveSingleItemsFromCurriculumLanguageAsync(curriculumID, existingLanguage.LanguageCode, newLanguage.LanguageCode);
                            }
                            var newOrder = Setting.CurriculumLanguages.Single(c => c.LanguageCode == newLanguage.LanguageCode).Order;
                            var oldOrder = existingCurriculumLanguages.Single(c => c.Language.LanguageCode == newLanguage.LanguageCode).Order;
                            existingCurriculumLanguages.Single(c => c.Order == newOrder).Order = oldOrder;
                            existingCurriculumLanguages.Single(c => c.Language.LanguageCode == newLanguage.LanguageCode).Order = newOrder;

                            ignoreOrder.Add(oldOrder);

                        }
                        curriculum.LastUpdated = DateTime.Now;
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
                        var nrOfItems = (await repository.CountItemsFromCurriculumAsync(curriculumID, newLanguage.LanguageCode)).Single().Value.Sum(i => i.Value);
                        Setting.SettingItems[i].Copy = false;
                        Setting.SettingItems[i].FormerLanguageCode = newLanguage.LanguageCode;
                        Setting.SettingItems[i].NrOfItems = nrOfItems;
                        ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.Copy)}", new ValueProviderResult("false", CultureInfo.InvariantCulture));
                        ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.FormerLanguageCode)}", new ValueProviderResult(newLanguage.LanguageCode, CultureInfo.InvariantCulture));
                        ModelState.SetModelValue($"{nameof(Setting)}.{nameof(Setting.SettingItems)}[{i}].{nameof(SettingItemVM.NrOfItems)}", new ValueProviderResult(nrOfItems.ToString(), CultureInfo.InvariantCulture));
                    }
                }
            }

            FillSelectionViewModel();

            return Page();
        }

        public async Task<RedirectResult> OnPostDeleteAccountAsync()
        {
            if (ModelState.IsValid)
            {
                // Delete
                var existingCurriculumLanguages = vitaeContext.CurriculumLanguages.Include(c => c.Language).Include(c => c.Curriculum).Where(cl => cl.CurriculumID == curriculumID).ToList();
                await DeleteLanguagesAsync(existingCurriculumLanguages, existingCurriculumLanguages.Select(e => e.Language.LanguageCode));

                vitaeContext.CurriculumLanguages.RemoveRange(vitaeContext.CurriculumLanguages.Include(c => c.Language).Where(c => c.CurriculumID == curriculumID));
                vitaeContext.Publications.RemoveRange(vitaeContext.Publications.Include(p => p.Curriculum).Where(p => p.Curriculum.CurriculumID == curriculumID));
                var curriculum = await repository.GetCurriculumAsync<PersonalDetail>(curriculumID);

                // Remove core values
                if (curriculum.PersonalDetails.Count == 1)
                {
                    curriculum.PersonalDetails.Single().Children.ToList().ForEach(c => vitaeContext.Entry(c).State = EntityState.Deleted);
                    curriculum.PersonalDetails.Single().PersonCountries.ToList().ForEach(p => vitaeContext.Entry(p).State = EntityState.Deleted);
                    vitaeContext.Entry(curriculum.PersonalDetails.Single()).State = EntityState.Deleted;
                }
                vitaeContext.Entry(vitaeContext.Curriculums.Single(c => c.CurriculumID == curriculumID)).State = EntityState.Deleted;

                await repository.LogActivityAsync(curriculumID, LogArea.Delete, LogLevel.Information, CodeHelper.GetCalledUri(httpContext), CodeHelper.GetUserAgent(httpContext), requestCulture.RequestCulture.UICulture.Name, httpContext.Connection.RemoteIpAddress.ToString());

                await vitaeContext.SaveChangesAsync();

                // Identity context
                var user = await userManager.GetUserAsync(User);
                identityContext.Users.Remove(identityContext.Users.Single(u => u.Id == user.Id));

                await identityContext.SaveChangesAsync();

                // Send mail
                var email = await userManager.GetEmailAsync(user);
                var bodyText = await CodeHelper.GetMailBodyTextAsync(SharedResource.AccountDeleted, email, new Tuple<string, string, string>(SharedResource.MailBye3, Globals.APPLICATION_URL, SharedResource.ClickingHere), false, SharedResource.Goodbye);
                var logoStream = CodeHelper.GetLogoStream(Globals.LOGO);
                var message = new Message(new string[] { email }, SharedResource.AccountDeleted, bodyText, new FormFileCollection() { new FormFile(logoStream, 0, logoStream.Length, "image/png", "logo") });
                await emailSender.SendEmailAsync(message);

                await signInManager.SignOutAsync();

                return Redirect("/");
            }
            else
            {
                return Redirect($"/{nameof(Manage)}/{nameof(Settings)}");
            }
        }

        #endregion

        #region AJAX
        public IActionResult OnPostLanguageChange()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SETTINGS);
        }

        public IActionResult OnPostAddCurriculumLanguage()
        {
            if (Setting.CurriculumLanguages.Count < MaxCurriculumLanguages)
            {
                Setting.SettingItems.Add(new SettingItemVM());
                Setting.CurriculumLanguages.Add(new CurriculumLanguageVM()
                {
                    Order = Setting.CurriculumLanguages.Count,
                    Collapsed = base.Collapsed
                });
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

        public IActionResult OnPostCollapse()
        {
            Collapse(Setting.CurriculumLanguages);

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
            if (ModelState.IsValid)
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