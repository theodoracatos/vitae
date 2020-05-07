using Library.Helper;
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
using Vitae.Code.PageModels;
using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Publications
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_PUBLICATION = "_Publication";

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public int MaxPublications { get; } = 10;

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Publications);

                Publications.Select(p => new Poco.Publication()
                {
                    Order = p.Order,
                    Anonymize = p.Anonymize,
                    Curriculum = curriculum,
                    Password = p.EnablePassword ? AesHandler.Encrypt(p.Password, p.PublicationIdentifier) : null,
                    PublicationIdentifier = Guid.Parse(p.PublicationIdentifier),
                    Notes = p.Notes,
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == p.LanguageCode),
                }).ToList().ForEach(p => curriculum.Publications.Add(p));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();

                // Set link & QR-Code
                Publications.Cast<PublicationVM>().ToList().ForEach(p =>
                {
                    p.Link = $"{this.BaseUrl}/CV/{p.PublicationIdentifier}?culture={p.LanguageCode.ToLower()}";
                    p.QrCode = CodeHelper.CreateQRCode($"{this.BaseUrl}/CV/{p.PublicationIdentifier}?culture={p.LanguageCode.ToLower()}");
                 });
            }

            FillSelectionViewModel();

            return Page();
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
                    Collapsed = base.Collapsed,
                    PublicationIdentifier = Guid.NewGuid().ToString()
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

        public async Task<IActionResult> OnPostDeletePublicationAsync(int order)
        {
            Delete(Publications, order);

            var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);

            var item = curriculum.Publications.SingleOrDefault(e => e.Order == order);
            if (item != null)
            {
                vitaeContext.Remove(item);
                curriculum.Publications.Where(e => e.Order > order).ToList().ForEach(e => e.Order = e.Order - 1);

                await vitaeContext.SaveChangesAsync();
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Publications);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        public IActionResult OnPostEnablePassword(int order)
        {
            Publications[order].Password = string.Empty;
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PUBLICATION);
        }

        #endregion

        #region HELPER

        private async Task LoadPublications(Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Publication>(curriculumID);

            Publications = repository.GetPublications(curriculum, this.BaseUrl);
        }

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}