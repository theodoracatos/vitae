﻿using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vitae.Helper;

namespace Vitae.Pages.Manage
{
    public class IndexModel : PageModel
    {
        private const string PAGE_INDEX_PERSONAL = "_Index_Personal";
        private const string PAGE_INDEX_ABOUT = "_Index_About";

        [BindProperty]
        public PersonVM Person { get; set; }
        [BindProperty]
        public AboutVM About { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
        public string PhonePrefix { get; set; }

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


        public IActionResult OnGet(Guid id)
        {
            // TODO: Check if id is from person x

            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum(id);
                Person = new PersonVM()
                {
                    Birthday = curriculum.Person.Birthday,
                    City = curriculum.Person.City,
                    CountryCode = curriculum.Person.Country.CountryCode,
                    Email = curriculum.Person.Email,
                    Firstname = curriculum.Person.Firstname,
                    Lastname = curriculum.Person.Lastname,
                    Gender = curriculum.Person.Gender,
                    LanguageCode = curriculum.Person.Language.LanguageCode,
                    MobileNumber = curriculum.Person.MobileNumber,
                    Street = curriculum.Person.Street,
                    StreetNo = curriculum.Person.StreetNo,
                    ZipCode = curriculum.Person.ZipCode,
                    State = curriculum.Person.State
                };
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

                FillSelectionViewModel();
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
        #endregion

        #region AJAX

        public async Task<IActionResult> OnPostSavePersonalAsync(Guid id)
        {
            // TODO: Check if id is from person x

            if (ModelState.IsValid(nameof(Person)))
            {
                var curriculum = GetCurriculum(id);
                curriculum.Person.Birthday = Person.Birthday;
                curriculum.Person.City = Person.City;
                curriculum.Person.Country = appContext.Countries.Single(c => c.CountryCode == Person.CountryCode);
                curriculum.Person.Email = Person.Email;
                curriculum.Person.Firstname = Person.Firstname;
                curriculum.Person.Lastname = Person.Lastname;
                curriculum.Person.Gender = Person.Gender.Value;
                curriculum.Person.Language = appContext.Languages.Single(l => l.LanguageCode == Person.LanguageCode);
                curriculum.Person.MobileNumber = Person.MobileNumber;
                curriculum.Person.Street = Person.Street;
                curriculum.Person.StreetNo = Person.StreetNo;
                curriculum.Person.ZipCode = Person.ZipCode;
                curriculum.Person.State = Person.State;

                await appContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INDEX_PERSONAL);
        }

        public IActionResult OnPostChangeCountry()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_INDEX_PERSONAL);
        }

        public async Task<IActionResult> OnPostSaveAbout(Guid id)
        {
            // TODO: Check if id is from person x
            if (ModelState.IsValid(nameof(About)))
            {
                var curriculum = GetCurriculum(id);
                curriculum.Person.About = curriculum.Person.About == null ? new About() : curriculum.Person.About;
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
                else if(About.Vfile?.FileName == null && About.Vfile.Identifier != Guid.Empty)
                {
                    appContext.Vfiles.Remove(appContext.Vfiles.Single(v => v.Identifier == About.Vfile.Identifier));
                    // Update VM
                    About.Vfile.Identifier = Guid.Empty;
                    About.Vfile.FileName = null;
                }

                await appContext.SaveChangesAsync();
            }

            FillSelectionViewModel();
            return GetPartialViewResult(PAGE_INDEX_ABOUT);
        }

        #endregion


        #region Helper

        private Curriculum GetCurriculum(Guid id)
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.Country)
                    .Include(c => c.Person.Language)
                    .Include(c => c.Person.About)
                    .Include(c => c.Person.About.Vfile)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }


        private void FillSelectionViewModel()
        {
            Countries = appContext.Countries.OrderBy(c => c.Name).Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.Culture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.Culture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.Culture.Name == "es" ? c.Name_es :
                        c.Name,
                PhoneCode = c.PhoneCode
            });

            Languages = appContext.Languages.OrderBy(c => c.Name).Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.Culture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.Culture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.Culture.Name == "es" ? c.Name_es :
                        c.Name
            });

            PhonePrefix = !string.IsNullOrEmpty(Person?.CountryCode) ? "+" + Countries.Where(c => c.CountryCode == Person.CountryCode).Select(c => c.PhoneCode).Single().ToString() : string.Empty;
        }

        private PartialViewResult GetPartialViewResult(string viewName)
        {
            // Ajax
            var dataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { nameof(IndexModel), this } };
            dataDictionary.Model = this;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = viewName,
                ViewData = dataDictionary,
            };

            return result;
        }
        #endregion
    }
}