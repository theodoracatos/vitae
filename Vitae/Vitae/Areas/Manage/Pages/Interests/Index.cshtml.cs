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

namespace Vitae.Areas.Manage.Pages.Interests
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_INTERESTS = "_Interests";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Interests), Prompt = nameof(SharedResource.Interests))]
        [BindProperty]
        public IList<InterestVM> Interests { get; set; }

        public int MaxInterests { get; } = 20;

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
                Interests = repository.GetInterests(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Interests);

                curriculum.Person.Interests =
                    Interests.Select(i => new Poco.Interest()
                    {
                        Description = i.Description,
                        Link = i.Link,
                        Name = i.Name,
                        Order = i.Order
                    }).ToList();
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddInterest()
        {
            if (Interests.Count < MaxInterests)
            {
                Interests.Add(new InterestVM() { Order = Interests.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostRemoveInterest()
        {
            if (Interests.Count > 0)
            {
                Interests.RemoveAt(Interests.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostUpInterest(int order)
        {
            var interest = Interests[order];
            Interests[order] = Interests[order - 1];
            Interests[order - 1] = interest;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostDownInterest(int order)
        {
            var interest = Interests[order];
            Interests[order] = Interests[order + 1];
            Interests[order + 1] = interest;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }
        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {}

        #endregion
    }
}