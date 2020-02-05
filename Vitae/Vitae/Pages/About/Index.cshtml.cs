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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;
using Poco = Persistency.Poco;

namespace Vitae.Pages.About
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_ABOUT = "_About";
        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // to be read from header

        [BindProperty]
        public AboutVM About { get; set; }

        private readonly IStringLocalizer<SharedResource> localizer;
        private readonly ApplicationContext appContext;
        private readonly IRequestCultureFeature requestCulture;

        public IndexModel(IStringLocalizer<SharedResource> localizer, ApplicationContext appContext, IHttpContextAccessor httpContextAccessor)
        {
            this.localizer = localizer;
            this.appContext = appContext;
            requestCulture = httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>();
        }

        #region SYNC

        public IActionResult OnGet()
        {
            // TODO: Check if id is from person x
            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();

                About = new AboutVM()
                {
                    Photo = curriculum.Person.About?.Photo,
                    Slogan = curriculum.Person.About?.Slogan,
                    Vfile = new VfileVM()
                    {
                        FileName = curriculum.Person.About?.Vfile?.FileName,
                        Identifier = curriculum.Person.About?.Vfile?.Identifier ?? Guid.Empty
                    }
                };

                return Page();
            }
        }

        public IActionResult OnGetOpenFile(Guid identifier)
        {
            if (appContext.Vfiles.Any(v => v.Identifier == identifier))
            {
                var vfile = appContext.Vfiles.Single(v => v.Identifier == identifier);

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
                var curriculum = GetCurriculum();
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
                    appContext.Vfiles.Remove(appContext.Vfiles.Single(v => v.Identifier == About.Vfile.Identifier));
                    // Update VM
                    About.Vfile.Identifier = Guid.Empty;
                    About.Vfile.FileName = null;
                }

                await appContext.SaveChangesAsync();
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
        private Curriculum GetCurriculum()
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.About)
                    .Include(c => c.Person.About.Vfile)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel() { }
        #endregion
    }
}