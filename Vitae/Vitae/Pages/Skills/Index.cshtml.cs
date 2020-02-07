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

namespace Vitae.Pages.Skills
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_SKILLS = "_Skills";
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly VitaeContext vitaeContext;
        private readonly IRequestCultureFeature requestCulture;

        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Skills), Prompt = nameof(SharedResource.Skills))]
        [BindProperty]
        public IList<SkillVM> Skills { get; set; }

        public int MaxSkills { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.vitaeContext = vitaeContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            if (id == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();

                Skills = curriculum.Person.Skills.OrderBy(ls => ls.Order)
                    .Select(s => new SkillVM()
                    {
                        Category = s.Category,
                        Order = s.Order,
                        Skillset = s.Skillset
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
                vitaeContext.RemoveRange(curriculum.Person.Skills);

                curriculum.Person.Skills =
                    Skills.Select(s => new Poco.Skill()
                    {
                        Category = s.Category,
                        Order = s.Order,
                        Skillset = s.Skillset
                    }).ToList();

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddSkill()
        {
            if (Skills.Count == 0)
            {
                Skills.Add(new SkillVM() { Order = 1 });
            }
            else if (Skills.Count < MaxSkills)
            {
                Skills.Add(new SkillVM() { Order = Skills.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostRemoveSkill()
        {
            if (Skills.Count > 0)
            {
                Skills.RemoveAt(Skills.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostUpSkill(int order)
        {
            var skill = Skills[order];
            Skills[order] = Skills[order - 1];
            Skills[order - 1] = skill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }

        public IActionResult OnPostDownSkill(int order)
        {
            var skill = Skills[order];
            Skills[order] = Skills[order + 1];
            Skills[order + 1] = skill;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_SKILLS);
        }
        #endregion

        #region Helper

        private Curriculum GetCurriculum()
        {
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Skills)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
        }

        #endregion
    }
}