using Library.Helper;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

using Poco = Model.Poco;

namespace Vitae.Areas.Manage.Pages.Abouts
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_ABOUTS = "_Abouts";

        [BindProperty]
        public AboutVM About { get; set; }

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
                var curriculum = await repository.GetCurriculumAsync<About>(curriculumID);
                CurriculumLanguageCode = CurriculumLanguageCode ?? curriculum.CurriculumLanguages.Single(c => c.Order == 0).Language.LanguageCode;
                
                LoadAbouts(curriculum, CurriculumLanguageCode);
                FillSelectionViewModel();

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
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                vitaeContext.RemoveRange(curriculum.Person.Abouts);

                var currentLanguage = curriculum.CurriculumLanguages.Single(cl => cl.Language.LanguageCode == CurriculumLanguageCode).Language;
                var about = curriculum.Person.Abouts.SingleOrDefault(pd => pd.CurriculumLanguage == currentLanguage) ?? new About() { };
                about.AcademicTitle = About.AcademicTitle;
                about.Slogan = About.Slogan;
                about.Photo = About.Photo;
                about.CurriculumLanguage = currentLanguage;

                if (About.Vfile?.Content != null)
                {
                    using (var stream = About.Vfile.Content.OpenReadStream())
                    {
                        if (CodeHelper.IsPdf(stream))
                        {
                            using (var reader = new BinaryReader(stream))
                            {
                                var identifier = Guid.NewGuid();
                                byte[] bytes = reader.ReadBytes((int)About.Vfile.Content.Length);
                                about.Vfile = new Vfile()
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
                }
                else if (About.Vfile?.FileName == null && About.Vfile.Identifier != Guid.Empty)
                {
                    vitaeContext.Vfiles.Remove(vitaeContext.Vfiles.Single(v => v.Identifier == About.Vfile.Identifier));
                    // Update VM
                    About.Vfile.Identifier = Guid.Empty;
                    About.Vfile.FileName = null;
                }

                curriculum.LastUpdated = DateTime.Now;
                curriculum.Person.Abouts.Add(about);
                await vitaeContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostRemoveFile()
        {
            //About.Vfile.FileName = null;

            return GetPartialViewResult(PAGE_ABOUTS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            var curriculum = await repository.GetCurriculumAsync(curriculumID);

            LoadAbouts(curriculum, CurriculumLanguageCode);
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABOUTS);
        }

        #endregion

        #region Helper
        protected override void FillSelectionViewModel() 
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private void LoadAbouts(Curriculum curriculum, string languageCode)
        {
            About = repository.GetAbouts(curriculum)
                   .SingleOrDefault(a => a.CurriculumLanguageCode == languageCode)
                   ?? new AboutVM()
                   {
                       Vfile = new VfileVM()
                       {
                           Identifier = Guid.Empty
                       }
                   };
        }
        #endregion
    }
}