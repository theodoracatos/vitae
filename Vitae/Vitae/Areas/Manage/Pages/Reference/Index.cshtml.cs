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
using Model.Poco;

using Vitae.Code;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Reference
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_REFERENCE = "_Reference";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Reference), Prompt = nameof(SharedResource.Reference))]
        [BindProperty]
        public IList<ReferenceVM> References { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }

        public int MaxReferences { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                References = repository.GetReferences(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
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

            return GetPartialViewResult(PAGE_REFERENCE);
        }

        public IActionResult OnPostAddReference()
        {
            if (References.Count < MaxReferences)
            {
                References.Add(new ReferenceVM() { Order = References.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCE);
        }

        public IActionResult OnPostRemoveReference()
        {
            if (References == null)
            {
                References = new List<ReferenceVM>() { };
            }
            else if (References.Count > 1)
            {
                References.RemoveAt(References.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCE);
        }

        public IActionResult OnPostUpReference(int order)
        {
            var reference = References[order];
            References[order] = References[order - 1];
            References[order - 1] = reference;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCE);
        }

        public IActionResult OnPostDownAbroad(int order)
        {
            var reference = References[order];
            References[order] = References[order + 1];
            References[order + 1] = reference;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCE);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
        }
        #endregion
    }
}