using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
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

namespace Vitae.Pages.Languages
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_LANGUAGES = "_Languages";
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Languages), Prompt = nameof(SharedResource.Languages))]
        [BindProperty]
        public List<LanguageSkillVM> LanguageSkills { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public int MaxLanguageSkills { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();

                LanguageSkills = curriculum.Person.LanguageSkills.OrderBy(ls => ls.Order)
                    .Select(l => new LanguageSkillVM()
                    {
                        LanguageCode = l.Language?.LanguageCode,
                        Order = l.Order,
                        Rate = l.Rate
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
                appContext.RemoveRange(curriculum.Person.LanguageSkills);

                curriculum.Person.LanguageSkills =
                    LanguageSkills.Select(e => new Poco.LanguageSkill()
                    {
                        Order = e.Order,
                        Rate = e.Rate,
                        Language = appContext.Languages.Single(l => l.LanguageCode == e.LanguageCode)
                    }).ToList();

                await appContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddLanguageSkill()
        {
            if (LanguageSkills.Count == 0)
            {
                LanguageSkills.Add(new LanguageSkillVM() { Order = 1 });
            }
            else if (LanguageSkills.Count < MaxLanguageSkills)
            {
                LanguageSkills.Add(new LanguageSkillVM() { Order = LanguageSkills.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostRemoveLanguageSkill()
        {
            if (LanguageSkills.Count > 0)
            {
                LanguageSkills.RemoveAt(LanguageSkills.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostUpLanguageSkill(int order)
        {
            var languageSkill = LanguageSkills[order];
            LanguageSkills[order] = LanguageSkills[order - 1];
            LanguageSkills[order - 1] = languageSkill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

        public IActionResult OnPostDownLanguageSkill(int order)
        {
            var languageSkill = LanguageSkills[order];
            LanguageSkills[order] = LanguageSkills[order + 1];
            LanguageSkills[order + 1] = languageSkill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_LANGUAGES);
        }
        #endregion

        #region Helper

        private Curriculum GetCurriculum()
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.LanguageSkills)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Languages = appContext.Languages.Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                        c.Name
            }).OrderBy(c => c.Name);
        }

        #endregion
    }
}