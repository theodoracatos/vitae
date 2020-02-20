﻿using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

namespace Vitae.Areas.Manage.Pages.Languages
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_LANGUAGES = "_Languages";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Languages), Prompt = nameof(SharedResource.Languages))]
        [BindProperty]
        public IList<LanguageSkillVM> LanguageSkills { get; set; }

        public IEnumerable<LanguageVM> Languages { get; set; }

        public int MaxLanguageSkills { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
            : base(localizer, vitaeContext, httpContextAccessor, userManager) { }

        #region SYNC
        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
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
                vitaeContext.RemoveRange(curriculum.Person.LanguageSkills);

                curriculum.Person.LanguageSkills =
                    LanguageSkills.Select(l => new Poco.LanguageSkill()
                    {
                        Order = l.Order,
                        Rate = l.Rate,
                        Language = vitaeContext.Languages.Single(la => la.LanguageCode == l.LanguageCode)
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

            return GetPartialViewResult(PAGE_LANGUAGES);
        }

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
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.LanguageSkills).ThenInclude(ls => ls.Language)
                    .Single(c => c.Identifier == curriculumID);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Languages = vitaeContext.Languages.Select(c => new LanguageVM()
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