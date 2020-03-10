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

namespace Vitae.Areas.Manage.Pages.References
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_REFERENCES = "_References";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Reference), Prompt = nameof(SharedResource.Reference))]
        [BindProperty]
        public IList<ReferenceVM> References { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxReferences { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                References = repository.GetReferences(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.References);

                curriculum.Person.References =
                    References.Select(r => new Poco.Reference()
                    {
                        CompanyName = r.CompanyName,
                        Email = r.Email,
                        Firstname = r.Firstname,
                        Lastname = r.Lastname,
                        PhoneNumber = r.PhoneNumber,
                        Order = r.Order,
                        Description = r.Description,
                        Gender = r.Gender.Value,
                        Link = r.Link,
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == r.CountryCode)
                    }).ToList();
                curriculum.LastUpdated = DateTime.Now;

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }

        #endregion

        #region AJAX
        public IActionResult OnPostSelectChange()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostAddReference()
        {
            if (References.Count < MaxReferences)
            {
                References.Add(new ReferenceVM() { Order = References.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostRemoveReference()
        {
            if (References.Count > 0)
            {
                References.RemoveAt(References.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostUpReference(int order)
        {
            var reference = References[order];
            References[order] = References[order - 1];
            References[order - 1] = reference;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostDownReference(int order)
        {
            var reference = References[order];
            References[order] = References[order + 1];
            References[order + 1] = reference;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            foreach(var reference in References)
            {
                reference.PhonePrefix = repository.GetPhonePrefix(reference.CountryCode);
            }
        }
        #endregion
    }
}