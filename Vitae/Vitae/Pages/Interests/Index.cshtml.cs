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

namespace Vitae.Pages.Interests
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_INTERESTS = "_Interests";
        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly VitaeContext vitaeContext;
        private readonly IRequestCultureFeature requestCulture;

        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Interests), Prompt = nameof(SharedResource.Interests))]
        [BindProperty]
        public IList<InterestVM> Interests { get; set; }

        public int MaxInterests { get; } = 20;

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

                Interests = curriculum.Person.Interests.OrderBy(ir => ir.Order)
                    .Select(i => new InterestVM()
                    {
                        Description = i.Description,
                        Link = i.Link,
                        Name = i.Name,
                        Order = i.Order
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
                vitaeContext.RemoveRange(curriculum.Person.Interests);

                curriculum.Person.Interests =
                    Interests.Select(i => new Poco.Interest()
                    {
                        Description = i.Description,
                        Link = i.Link,
                        Name = i.Name,
                        Order = i.Order
                    }).ToList();

                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return Page();
        }
        #endregion

        #region AJAX
        public IActionResult OnPostAddInterest()
        {
            if (Interests.Count == 0)
            {
                Interests.Add(new InterestVM() { Order = 1 });
            }
            else if (Interests.Count < MaxInterests)
            {
                Interests.Add(new InterestVM() { Order = Interests.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostRemoveInterest()
        {
            if (Interests.Count > 0)
            {
                Interests.RemoveAt(Interests.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostUpInterest(int order)
        {
            var interest = Interests[order];
            Interests[order] = Interests[order - 1];
            Interests[order - 1] = interest;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }

        public IActionResult OnPostDownInterest(int order)
        {
            var interest = Interests[order];
            Interests[order] = Interests[order + 1];
            Interests[order + 1] = interest;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INTERESTS);
        }
        #endregion

        #region Helper

        private Curriculum GetCurriculum()
        {
            var curriculum = vitaeContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Interests)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {

        }

        #endregion
    }
}