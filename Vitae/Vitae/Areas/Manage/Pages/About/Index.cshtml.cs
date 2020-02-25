using Library.Repository;
using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Persistency.Poco;

namespace Vitae.Areas.Manage.Pages.About
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_ABOUT = "_About";

        [BindProperty]
        public AboutVM About { get; set; }

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
                About = repository.GetAboutVM(curriculumID);

                return Page();
            }
        }

        public IActionResult OnGetOpenFile(Guid identifier)
        {
            if (vitaeContext.Vfiles.Any(v => v.Identifier == identifier))
            {
                var vfile = vitaeContext.Vfiles.Single(v => v.Identifier == identifier);

                return File(vfile.Content, vfile.MimeType, vfile.FileName);
            }
            else
            {
                throw new FileNotFoundException(identifier.ToString());
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // TODO: Check if id is from person x
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                curriculum.Person.About = curriculum.Person.About == null ? new Poco.About() : curriculum.Person.About;
                curriculum.Person.About.Slogan = About.Slogan;
                curriculum.Person.About.Photo = About.Photo;

                if (About.Vfile?.Content != null)
                {
                    using (var stream = About.Vfile.Content.OpenReadStream())
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            var identifier = Guid.NewGuid();
                            byte[] bytes = reader.ReadBytes((int)About.Vfile.Content.Length);
                            curriculum.Person.About.Vfile = new Vfile()
                            {
                                Content = bytes,
                                FileName = About.Vfile.Content.FileName,
                                Identifier = identifier,
                                MimeType = "application/pdf"
                            };
                            // Update VM
                            About.Vfile.Identifier = identifier;
                            About.Vfile.FileName = About.Vfile.Content.FileName;
                        }
                    }
                }
                else if (About.Vfile?.FileName == null && About.Vfile.Identifier != Guid.Empty)
                {
                    vitaeContext.Vfiles.Remove(vitaeContext.Vfiles.Single(v => v.Identifier == About.Vfile.Identifier));
                    // Update VM
                    About.Vfile.Identifier = Guid.Empty;
                    About.Vfile.FileName = null;
                }

                await vitaeContext.SaveChangesAsync();
            }

            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostRemoveFile()
        {
            About.Vfile.FileName = null;

            return GetPartialViewResult(PAGE_ABOUT);
        }
        #endregion

        #region Helper
        protected override void FillSelectionViewModel() { }
        #endregion
    }
}