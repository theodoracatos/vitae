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
        public PersonVM Person { get; set; }

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
                Person = repository.GetPerson(curriculum, identity.Name);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = repository.GetCurriculum(curriculumID);
                curriculum.Person = curriculum.Person ?? new Person() { PersonCountries = new List<PersonCountry>() };
                curriculum.Person.Birthday = new DateTime(Person.Birthday_Year, Person.Birthday_Month, Person.Birthday_Day);
                curriculum.Person.City = Person.City;
                curriculum.Person.Country = vitaeContext.Countries.Single(c => c.CountryCode == Person.CountryCode);
                curriculum.Person.Email = Person.Email;
                curriculum.Person.Firstname = Person.Firstname;
                curriculum.Person.Lastname = Person.Lastname;
                curriculum.Person.Gender = Person.Gender.Value;
                curriculum.Person.Language = vitaeContext.Languages.Single(l => l.LanguageCode == Person.LanguageCode);
                curriculum.Person.MobileNumber = Person.MobileNumber;
                curriculum.Person.Street = Person.Street;
                curriculum.Person.StreetNo = Person.StreetNo;
                curriculum.Person.ZipCode = Person.ZipCode;
                curriculum.Person.State = Person.State;

                // Nationality
                curriculum.Person.PersonCountries.Clear();
                foreach (var nationality in Person.Nationalities)
                {
                    var personCountry = new PersonCountry()
                    {
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode),
                        CountryID = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode).CountryID,
                        Person = curriculum.Person,
                        PersonID = curriculum.Person.PersonID,
                        Order = Person.Nationalities.IndexOf(nationality)
                    };
                    curriculum.Person.PersonCountries.Add(personCountry);
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
            if (Person.Nationalities == null)
            {
                Person.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 0 } };
            }
            else if (Person.Nationalities.Count < MaxNationalities)
            {
                Person.Nationalities.Add(new NationalityVM() { Order = Person.Nationalities.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostRemoveNationality()
        {
            if (Person.Nationalities == null)
            {
                Person.Nationalities = new List<NationalityVM>() { };
            }
            else if (Person.Nationalities.Count > 1)
            {
                Person.Nationalities.RemoveAt(Person.Nationalities.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostAddChild()
        {
            if (Person.Children == null)
            {
                Person.Children = new List<ChildVM>() { new ChildVM() { Order = 0 } };
            }
            else if (Person.Children.Count < MaxChildren)
            {
                Person.Children.Add(new ChildVM() { Order = Person.Children.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostRemoveChild()
        {
            if (Person.Children == null)
            {
                Person.Children = new List<ChildVM>() { };
            }
            else if (Person.Children.Count > 1)
            {
                Person.Children.RemoveAt(Person.Children.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostChangeBirthday()
        {
            Person.Birthday_Day = CorrectBirthday(Person.Birthday_Year, Person.Birthday_Month, Person.Birthday_Day);
           
            foreach(var child in Person.Children)
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

            if (Person.Nationalities == null || Person.Nationalities.Count == 0)
            {
                Person.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 0 } };
            }
        }
        #endregion
    }
}