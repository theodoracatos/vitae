using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

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

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
            : base(localizer, vitaeContext, httpContextAccessor, userManager) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();

                Awards = curriculum.Person.Awards.OrderBy(aw => aw.Order)
                    .Select(a => new AwardVM()
                    {
                        AwardedFrom = a.AwardedFrom,
                        Description = a.Description,
                        Link = a.Link,
                        Month = a.AwardedOn.Month,
                        Year = a.AwardedOn.Year,
                        Name = a.Name,
                        Order = a.Order
                    }).ToList();

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = GetCurriculum();
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

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddAward()
        {
            if (Awards.Count == 0)
            {
                Awards.Add(new AwardVM() { Order = 1 });
            }
            else if (Awards.Count < MaxAwards)
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

        private Curriculum GetCurriculum()
        {
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Awards)
                    .Single(c => c.Identifier == curriculumID);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Months = vitaeContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);
        }

        #endregion
    }
}