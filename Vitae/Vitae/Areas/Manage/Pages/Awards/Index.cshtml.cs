using Library.Extensions;
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

namespace Vitae.Areas.Manage.Pages.Awards
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_AWARDS = "_Awards";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Awards), Prompt = nameof(SharedResource.Awards))]
        [BindProperty]
        public IList<AwardVM> Awards { get; set; }

        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxAwards { get; } = 20;

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

                await LoadAwardsAsync();
                FillSelectionViewModel();

                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Award>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Awards.Where(a => a.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Awards.Select(a => new Poco.Award()
                    {
                        AwardedFrom = a.AwardedFrom,
                        AwardedOn = new DateTime(a.Year, a.Month, 1),
                        Description = a.Description,
                        Link = a.Link,
                        Name = a.Name,
                        Order = a.Order,
                        CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                    }).ToList().ForEach(a => curriculum.Awards.Add(a));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddAward()
        {
            if (Awards.Count < MaxAwards)
            {
                Awards.Add(new AwardVM() 
                { 
                    Order = Awards.Count,
                    Collapsed = base.Collapsed
                });
                Awards = CheckOrdering(Awards);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostRemoveAward()
        {
            Remove(Awards);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostUpAward(int order)
        {
            Up(Awards, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostDownAward(int order)
        {
            Down(Awards, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostDeleteAward(int order)
        {
            Delete(Awards, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostCopyAward(int order)
        {
            if (Awards.Count < MaxAwards)
            {
                var copy = Awards[order].DeepClone();
                copy.Order = Awards.Count;
                Awards.Add(copy);
                Awards = CheckOrdering(Awards);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Awards);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadAwardsAsync();
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadAwardsAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<Award>(curriculumID);

            Awards = repository.GetAwards(curriculum, CurriculumLanguageCode);
        }

        #endregion
    }
}