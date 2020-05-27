using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Vitae.Code.PageModels;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Publications
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_PUBLICATION = "_Publication";

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

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
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                await LoadPublicationsAsync();

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnGetDownloadQrCodeAsync(int index)
        {
            var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);
            await LoadPublicationsAsync();

            var base64 = Publications[index].QrCode.Substring(22, Publications[index].QrCode.Length - 22);
            byte[] bytes = Convert.FromBase64String(base64);

            return File(bytes, "application/octet-stream", $"QrCode_{Publications[index].PublicationIdentifier.Substring(0, Publications[index].PublicationIdentifier.IndexOf("-"))}.png");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Delete
                var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);
                var publicationIds = Publications.Select(pu => pu.PublicationIdentifier).ToList();
                var deletedPublications = curriculum.Publications.Where(p => !publicationIds.Contains(p.PublicationIdentifier.ToString())).ToList();

                foreach (var publication in deletedPublications)
                {
                    var item = curriculum.Publications.Single(e => e.PublicationIdentifier == publication.PublicationIdentifier);
                    vitaeContext.Remove(item);

                    await vitaeContext.SaveChangesAsync();
                }

                // Add
                vitaeContext.RemoveRange(curriculum.Publications);

                Publications.Select(p => new Poco.Publication()
                {
                    Order = p.Order,
                    Anonymize = p.Anonymize,
                    Secure = p.Secure,
                    Curriculum = curriculum,
                    Password = p.EnablePassword ? AesHandler.Encrypt(p.Password, p.PublicationIdentifier) : null,
                    PublicationIdentifier = Guid.Parse(p.PublicationIdentifier),
                    Notes = p.Notes,
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == p.LanguageCode),
                    Color = p.Color
                }).ToList().ForEach(p => curriculum.Publications.Add(p));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();

                // Set link & QR-Code
                Publications.Cast<PublicationVM>().ToList().ForEach(p =>
                {
                    p.Link = $"{this.BaseUrl}/CV/{p.PublicationIdentifier}?culture={p.LanguageCode.ToLower()}";
                    p.QrCode = CodeHelper.CreateQRCode($"{this.BaseUrl}/CV/{p.PublicationIdentifier}?culture={p.LanguageCode.ToLower()}");

                    // Update ModelState too
                    ModelState.SetModelValue($"{nameof(IndexModel.Publications)}[{Publications.IndexOf(p)}].{nameof(PublicationVM.Link)}", new ValueProviderResult(p.Link, CultureInfo.InvariantCulture));
                    ModelState.SetModelValue($"{nameof(IndexModel.Publications)}[{Publications.IndexOf(p)}].{nameof(PublicationVM.QrCode)}", new ValueProviderResult(p.QrCode, CultureInfo.InvariantCulture));
                });

                // Update View Model

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
                    PublicationIdentifier = Guid.NewGuid().ToString(),
                    Color = Globals.DEFAULT_BACKGROUND_COLOR
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

        private async Task LoadPublicationsAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<Publication>(curriculumID);

            Publications = repository.GetPublications(curriculum, this.BaseUrl);
        }

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}