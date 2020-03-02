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

namespace Vitae.Areas.Manage.Pages.Awards
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_AWARDS = "_Awards";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Awards), Prompt = nameof(SharedResource.Awards))]
        [BindProperty]
        public IList<AwardVM> Awards { get; set; }

        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxAwards { get; } = 20;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                Awards = repository.GetAwards(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Awards);

                curriculum.Person.Awards =
                    Awards.Select(a => new Poco.Award()
                    {
                        AwardedFrom = a.AwardedFrom,
                        AwardedOn = new DateTime(a.Year, a.Month, 1),
                        Description = a.Description,
                        Link = a.Link,
                        Name = a.Name,
                        Order = a.Order
                    }).ToList();
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
                Awards.Add(new AwardVM() { Order = Awards.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostRemoveAward()
        {
            if (Awards.Count > 0)
            {
                Awards.RemoveAt(Awards.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostUpAward(int order)
        {
            var award = Awards[order];
            Awards[order] = Awards[order - 1];
            Awards[order - 1] = award;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }

        public IActionResult OnPostDownAward(int order)
        {
            var award = Awards[order];
            Awards[order] = Awards[order + 1];
            Awards[order + 1] = award;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_AWARDS);
        }
        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}