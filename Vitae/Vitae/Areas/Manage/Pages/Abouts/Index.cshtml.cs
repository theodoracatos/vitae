﻿using Library.Constants;
using Library.Helper;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Vitae.Code.Mailing;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Manage.Pages.Abouts
{
    [RequestSizeLimit(100_000_000)]
    public class IndexModel : BasePageModel
    {
        public const string PAGE_ABOUTS = "_Abouts";

        [BindProperty]
        public AboutVM About { get; set; }

        public IndexModel(IHttpClientFactory clientFactory, IConfiguration configuration, IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository, SignInManager<IdentityUser> signInManager, IEmailSender emailSender)
    : base(clientFactory, configuration, localizer, vitaeContext, httpContextAccessor, userManager, repository, signInManager, emailSender) { }

        #region SYNC

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            else
            {
                LoadLanguageCode();

                await LoadAboutsAsync();
                FillSelectionViewModel();

                return Page();
            }
        }

        public IActionResult OnGetOpenFile(Guid identifier)
        {
            if (vitaeContext.Vfiles.Any(v => v.VfileID == identifier))
            {
                var vfile = vitaeContext.Vfiles.Single(v => v.VfileID == identifier);

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
                var curriculum = await repository.GetCurriculumAsync<About>(curriculumID);

                var about = curriculum.Abouts.SingleOrDefault(a => a.CurriculumLanguage.LanguageCode == CurriculumLanguageCode) ?? new About() { };
                about.AcademicTitle = About.AcademicTitle;
                about.Slogan = About.Slogan;
                about.Photo = About.Photo;
                about.CurriculumLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == CurriculumLanguageCode);

                if (About.Vfile?.Content != null)
                {
                    using var stream = About.Vfile.Content.OpenReadStream();
                    if (CodeHelper.IsZip(stream) || CodeHelper.IsGZip(stream))
                    {
                        var existingVFile = repository.GetAbouts(curriculum, CurriculumLanguageCode).Single().Vfile;
                        if (existingVFile.Identifier != Guid.Empty)
                        {
                            // Remove existing file
                            vitaeContext.Vfiles.Remove(vitaeContext.Vfiles.Single(v => v.VfileID == existingVFile.Identifier));

                            await vitaeContext.SaveChangesAsync();
                        }

                        using (var reader = new BinaryReader(stream))
                        {
                            byte[] bytes = reader.ReadBytes((int)About.Vfile.Content.Length);
                            about.Vfile = new Vfile()
                            {
                                Content = bytes,
                                FileName = About.Vfile.Content.FileName,
                                MimeType = Globals.MIME_ZIP
                            };
                        }
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status422UnprocessableEntity);
                    }
                }
                else if (About.Vfile?.FileName == null && About.Vfile.Identifier != Guid.Empty)
                {
                    // Remove file
                    vitaeContext.Vfiles.Remove(vitaeContext.Vfiles.Single(v => v.VfileID == About.Vfile.Identifier));

                    // Update VM
                    About.Vfile.Identifier = Guid.Empty;
                    About.Vfile.FileName = null;
                }

                curriculum.LastUpdated = DateTime.Now;
                curriculum.Abouts.Add(about);

                await vitaeContext.SaveChangesAsync();

                // UpdateVM
                if (About.Vfile?.Content != null)
                {
                    About.Vfile.FileName = About.Vfile.Content.FileName;
                    About.Vfile.Identifier = about.Vfile.VfileID;
                }
            }

            FillSelectionViewModel();

            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostRemoveFile()
        {
            About.Vfile.FileName = null;

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABOUTS);
        }

        public async Task<IActionResult> OnPostLanguageChangeAsync()
        {
            await SaveLanguageChangeAsync();

            await LoadAboutsAsync();
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_ABOUTS);
        }

        #endregion

        #region Helper
        protected override void FillSelectionViewModel() 
        {
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }

        private async Task LoadAboutsAsync()
        {
            var curriculum = await repository.GetCurriculumAsync<About>(curriculumID);
            About = repository.GetAbouts(curriculum, CurriculumLanguageCode).SingleOrDefault()
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