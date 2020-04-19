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
                var curriculum = await repository.GetCurriculumAsync<Reference>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;

                await LoadReferences(CurriculumLanguageCode, curriculum);
                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<Reference>(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.References.Where(r => r.CurriculumLanguage.LanguageCode == CurriculumLanguageCode));

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
                    Hide = r.Hide,
                    Country = vitaeContext.Countries.Single(c => c.CountryCode == r.CountryCode),
                    CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode)
                }).ToList().ForEach(r => curriculum.Person.References.Add(r));
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
                References.Add(new ReferenceVM() 
                { 
                    Order = References.Count,
                    Collapsed = base.Collapsed
                });
                References = CheckOrdering(References);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostRemoveReference()
        {
            Remove(References);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostUpReference(int order)
        {
            Up(References, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostDownReference(int order)
        {
            Down(References, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostDeleteReference(int order)
        {
            Delete(References, order);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostCopyReference(int order)
        {
            if (References.Count < MaxReferences)
            {
                var copy = References[order].DeepClone();
                copy.Order = References.Count;
                References.Add(copy);
                References = CheckOrdering(References);
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public IActionResult OnPostCollapse()
        {
            Collapse(References);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await LoadReferences(CurriculumLanguageCode);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_REFERENCES, hasUnsafedChanges: false);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            foreach(var reference in References)
            {
                reference.PhonePrefix = repository.GetPhonePrefix(reference.CountryCode);
            }
        }

        private async Task LoadReferences(string languageCode, Curriculum curr = null)
        {
            var curriculum = curr ?? await repository.GetCurriculumAsync<Reference>(curriculumID);

            References = repository.GetReferences(curriculum, languageCode);
        }
        #endregion
    }
}