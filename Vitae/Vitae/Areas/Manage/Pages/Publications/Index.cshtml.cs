using Library.Extensions;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vitae.Code;

namespace Vitae.Areas.Manage.Pages.Publications
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_PUBLICATION = "_Publication";

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public int MaxPublications { get; } = 5;

        [BindProperty]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Publications), Prompt = nameof(SharedResource.Publications))]
        public IList<PublicationVM> Publications { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);

                await LoadPublications(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        #endregion

        #region ASYNC

        public IActionResult OnPostAddPublication()
        {
            if (Publications.Count < MaxPublications)
            {
                Publications.Add(new PublicationVM()
                {
                    Order = Publications.Count,
                    Collapsed = base.Collapsed
                });
                Publications = CheckOrdering(Publications);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostRemovePublication()
        {
            Remove(Publications);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostUpPublication(int order)
        {
            Up(Publications, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostDownPublication(int order)
        {
            Down(Publications, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostDeletePublication(int order)
        {
            Delete(Publications, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        #endregion

        #region HELPER

        private async Task LoadPublications(Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Publication>(curriculumID);

            Publications = repository.GetPublications(curriculum);
        }

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}