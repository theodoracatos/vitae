﻿using Library.Extensions;
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

namespace Vitae.Areas.Manage.Pages.Certificates
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_CERTIFICATES = "_Certificates";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Certificates), Prompt = nameof(SharedResource.Certificates))]
        [BindProperty]
        public IList<CertificateVM> Certificates { get; set; }

        public int MaxCertificates { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

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

                await LoadCertificatesAsync();
                FillSelectionViewModel();

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Certificate>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Certificates.Where(c => c.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

                Certificates.Select(c => new Poco.Certificate()
                {
                    IssuedOn = new DateTime(c.Start_Year, c.Start_Month, 1),
                    ExpiresOn = c.NeverExpires ? null : (DateTime?)new DateTime(c.End_Year.Value, c.End_Month.Value, 1),
                    Order = c.Order,
                    Issuer = c.Issuer,
                    Description = c.Description,
                    Link = c.Link,
                    Name = c.Name,
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(c => curriculum.Certificates.Add(c));
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeNeverExpires()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostAddCertificate()
        {
            if (Certificates.Count < MaxCertificates)
            {
                Certificates.Add(new CertificateVM() {
                    Order = Certificates.Count,
                    Start_Month = DateTime.Now.Month,
                    Start_Year = DateTime.Now.Year,
                    End_Month = DateTime.Now.Month,
                    End_Year = DateTime.Now.Year,
                    Collapsed = base.Collapsed
                });
                Certificates = CheckOrdering(Certificates);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostRemoveCertificate()
        {
            Remove(Certificates);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostUpCertificate(int order)
        {
            Up(Certificates, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostDownCertificate(int order)
        {
            Down(Certificates, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostDeleteCertificate(int order)
        {
            Delete(Certificates, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostCopyCertificate(int order)
        {
            if (Certificates.Count < MaxCertificates)
            {
                var copy = Certificates[order].DeepClone();
                copy.Order = Certificates.Count;
                Certificates.Add(copy);
                Certificates = CheckOrdering(Certificates);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(Certificates);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadCertificatesAsync();

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadCertificatesAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<Certificate>(curriculumID);

            Certificates = repository.GetCertificates(curriculum, CurriculumLanguageCode);
        }

        #endregion
    }
}