﻿using Library.Repository;
using Library.Resources;
using Model.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Persistency.Data;
using Model.Poco;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

namespace Vitae.Areas.Manage.Pages.Personal
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_PERSONAL = "_Personal";

        [BindProperty]
        public PersonalDetailVM PersonalDetail { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }

        public IEnumerable<CountryVM> Nationalities { get; set; }
        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxChildren { get; } = 10;

        public int MaxNationalities { get; } = 3;

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
            : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        #region SYNC

        public IActionResult OnGet()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                PersonalDetail = repository.GetPersonalDetail(curriculum, identity.Name);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                curriculum.Person.PersonalDetail = curriculum.Person.PersonalDetail ?? new PersonalDetail() { PersonCountries = new List<PersonCountry>() };
                curriculum.Person.PersonalDetail.Birthday = new DateTime(PersonalDetail.Birthday_Year, PersonalDetail.Birthday_Month, PersonalDetail.Birthday_Day);
                curriculum.Person.PersonalDetail.City = PersonalDetail.City;
                curriculum.Person.PersonalDetail.Country = vitaeContext.Countries.Single(c => c.CountryCode == PersonalDetail.CountryCode);
                curriculum.Person.PersonalDetail.Email = PersonalDetail.Email;
                curriculum.Person.PersonalDetail.Firstname = PersonalDetail.Firstname;
                curriculum.Person.PersonalDetail.Lastname = PersonalDetail.Lastname;
                curriculum.Person.PersonalDetail.Gender = PersonalDetail.Gender.Value;
                curriculum.Person.PersonalDetail.Language = vitaeContext.Languages.Single(l => l.LanguageCode == PersonalDetail.LanguageCode);
                curriculum.Person.PersonalDetail.MobileNumber = PersonalDetail.MobileNumber;
                curriculum.Person.PersonalDetail.Street = PersonalDetail.Street;
                curriculum.Person.PersonalDetail.StreetNo = PersonalDetail.StreetNo;
                curriculum.Person.PersonalDetail.ZipCode = PersonalDetail.ZipCode;
                curriculum.Person.PersonalDetail.State = PersonalDetail.State;

                // Nationality
                curriculum.Person.PersonalDetail.PersonCountries.Clear();
                foreach (var nationality in PersonalDetail.Nationalities)
                {
                    var personCountry = new PersonCountry()
                    {
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode),
                        CountryID = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode).CountryID,
                        PersonalDetail = curriculum.Person.PersonalDetail,
                        PersonalDetailID = curriculum.Person.PersonalDetail.PersonalDetailID,
                        Order = PersonalDetail.Nationalities.IndexOf(nationality)
                    };
                    curriculum.Person.PersonalDetail.PersonCountries.Add(personCountry);
                }

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

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostAddNationality()
        {
            if (PersonalDetail.Nationalities == null)
            {
                PersonalDetail.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 0 } };
            }
            else if (PersonalDetail.Nationalities.Count < MaxNationalities)
            {
                PersonalDetail.Nationalities.Add(new NationalityVM() { Order = PersonalDetail.Nationalities.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostRemoveNationality()
        {
            if (PersonalDetail.Nationalities == null)
            {
                PersonalDetail.Nationalities = new List<NationalityVM>() { };
            }
            else if (PersonalDetail.Nationalities.Count > 1)
            {
                PersonalDetail.Nationalities.RemoveAt(PersonalDetail.Nationalities.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostAddChild()
        {
            if (PersonalDetail.Children == null)
            {
                PersonalDetail.Children = new List<ChildVM>() { new ChildVM() { Order = 0 } };
            }
            else if (PersonalDetail.Children.Count < MaxChildren)
            {
                PersonalDetail.Children.Add(new ChildVM() { Order = PersonalDetail.Children.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostRemoveChild()
        {
            if (PersonalDetail.Children == null)
            {
                PersonalDetail.Children = new List<ChildVM>() { };
            }
            else if (PersonalDetail.Children.Count > 1)
            {
                PersonalDetail.Children.RemoveAt(PersonalDetail.Children.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostChangeBirthday()
        {
            PersonalDetail.Birthday_Day = CorrectBirthday(PersonalDetail.Birthday_Year, PersonalDetail.Birthday_Month, PersonalDetail.Birthday_Day);
           
            foreach(var child in PersonalDetail.Children)
            {
                child.Birthday_Day = CorrectBirthday(child.Birthday_Year, child.Birthday_Month, child.Birthday_Day);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostChangeCountry()
        {
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        #endregion

        #region Helper

        private int CorrectBirthday(int year, int month, int day)
        {
            DateTime tempDate;
            do
            {
                if (!DateTime.TryParse($"{year}-{month}-{day}", out tempDate))
                {
                    --day; // Decrement (wrong day)
                }
            } while (tempDate == DateTime.MinValue);

            return day;
        }

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Nationalities = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);

            if (PersonalDetail.Nationalities == null || PersonalDetail.Nationalities.Count == 0)
            {
                PersonalDetail.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 0 } };
            }
        }
        #endregion
    }
}