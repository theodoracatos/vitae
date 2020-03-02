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

namespace Vitae.Areas.Manage.Pages.Certificates
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_CERTIFICATES = "_Certificates";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Certificates), Prompt = nameof(SharedResource.Certificates))]
        [BindProperty]
        public IList<CertificateVM> Certificates { get; set; }

        public int MaxCertificates { get; } = 20;

        public IEnumerable<MonthVM> Months { get; set; }

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
                Certificates = repository.GetCertificates(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Certificates);

                curriculum.Person.Certificates =
                    Certificates.Select(c => new Poco.Certificate()
                    {
                        IssuedOn = new DateTime(c.Start_Year, c.Start_Month, 1),
                        ExpiresOn = c.NeverExpires ? null : (DateTime?)new DateTime(c.End_Year.Value, c.End_Month.Value, 1),
                        Order = c.Order,
                        Issuer = c.Issuer,
                        Description = c.Description,
                        Link = c.Link,
                        Name = c.Name
                    }).ToList();
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX

        public IActionResult OnPostChangeNeverExpires(int order)
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
                    End_Year = DateTime.Now.Year
                });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostRemoveCertificate()
        {
            if (Certificates.Count > 0)
            {
                Certificates.RemoveAt(Certificates.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostUpCertificate(int order)
        {
            var certificate = Certificates[order];
            Certificates[order] = Certificates[order - 1];
            Certificates[order - 1] = certificate;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
        }

        public IActionResult OnPostDownCertificate(int order)
        {
            var certificate = Certificates[order];
            Certificates[order] = Certificates[order + 1];
            Certificates[order + 1] = certificate;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_CERTIFICATES);
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