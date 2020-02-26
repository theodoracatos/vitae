﻿using Library.Repository;
using Library.Resources;
using Library.ViewModels;

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

using Poco = Persistency.Poco;

namespace Vitae.Areas.Manage.Pages.Skills
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_SKILLS = "_Skills";

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Skills), Prompt = nameof(SharedResource.Skills))]
        [BindProperty]
        public IList<SkillVM> Skills { get; set; }

        public int MaxSkills { get; } = 10;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else if (vitaeContext.Curriculums.Include(c => c.Person).Single(c => c.Identifier == curriculumID).Person == null)
            {
                return BadRequest();
            }
            else
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                Skills = repository.GetSkills(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
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

        protected override void FillSelectionViewModel()
        {}

        #endregion
    }
}