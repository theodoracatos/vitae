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
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

namespace Vitae.Pages.Personal
{
    public class IndexModel : BasePageModel
    {
        private const string PAGE_PERSONAL = "_Personal";
        private Guid id = Guid.Parse("a05c13a8-21fb-42c9-a5bc-98b7d94f464a"); // TODO: to be read from header

        [BindProperty]
        public PersonVM Person { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
        public IEnumerable<CountryVM> Nationalities { get; set; }
        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxNationalities { get; } = 3;
        public int YearStart { get; } = 1900;
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

        public IActionResult OnGet()
        {
            if (id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                var curriculum = GetCurriculum();
                Person = new PersonVM()
                {
                    Birthday_Day = curriculum.Person.Birthday.Value.Day,
                    Birthday_Month = curriculum.Person.Birthday.Value.Month,
                    Birthday_Year = curriculum.Person.Birthday.Value.Year,
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
                    State = curriculum.Person.State,
                    Nationalities = curriculum.Person.PersonCountries?.OrderBy(pc => pc.Order)
                    .Select(n => new NationalityVM { CountryCode = n.Country.CountryCode, Order = n.Order } )
                    .ToList() ?? new List<NationalityVM>() { new NationalityVM() { Order = 1 } }
                };

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = GetCurriculum();
                curriculum.Person.Birthday = new DateTime(Person.Birthday_Year, Person.Birthday_Month, Person.Birthday_Day);
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

                // Nationality
                curriculum.Person.PersonCountries.Clear();
                foreach (var nationality in Person.Nationalities)
                {
                    var personCountry = new PersonCountry()
                    {
                        Country = appContext.Countries.Single(c => c.CountryCode == nationality.CountryCode),
                        CountryID = appContext.Countries.Single(c => c.CountryCode == nationality.CountryCode).CountryID,
                        Person = curriculum.Person,
                        PersonID = curriculum.Person.PersonID,
                        Order = Person.Nationalities.IndexOf(nationality)
                    };
                    curriculum.Person.PersonCountries.Add(personCountry);
                }

                await appContext.SaveChangesAsync();
            }

            FillSelectionViewModel();

            return Page();
        }

        #endregion

        #region AJAX

        public IActionResult OnPostAddNationality()
        {
            if (Person.Nationalities == null)
            {
                Person.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 1 } };
            }
            else if (Person.Nationalities.Count < MaxNationalities)
            {
                Person.Nationalities.Add(new NationalityVM() { Order = Person.Nationalities.Count + 1 });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostRemoveNationality()
        {
            if (Person.Nationalities == null)
            {
                Person.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 1 } };
            }
            else if (Person.Nationalities.Count > 1)
            {
                Person.Nationalities.RemoveAt(Person.Nationalities.Count - 1);
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONAL);
        }

        public IActionResult OnPostChangeBirthday()
        {
            DateTime tempDate;
            do
            {
                if (!DateTime.TryParse($"{Person.Birthday_Year}-{Person.Birthday_Month}-{Person.Birthday_Day}", out tempDate))
                {
                    --Person.Birthday_Day; // Decrement (wrong day)
                }
            } while (tempDate == DateTime.MinValue);

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

        private Curriculum GetCurriculum()
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.PersonCountries).ThenInclude(pc => pc.Country)
                    .Include(c => c.Person.Country)
                    .Include(c => c.Person.Language)
                    .Single(c => c.Identifier == id);

            return curriculum;
        }

        protected override void FillSelectionViewModel()
        {
            Countries = appContext.Countries.Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                        c.Name,
                PhoneCode = c.PhoneCode
            }).OrderBy(c => c.Name);

            Languages = appContext.Languages.Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
                        requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
                        requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
                        requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
                        c.Name
            }).OrderBy(c => c.Name);

            Nationalities = appContext.Countries.Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = requestCulture.RequestCulture.UICulture.Name == "de" ? c.Name_de :
            requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
            requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
            requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
            c.Name,
                PhoneCode = c.PhoneCode
            }).OrderBy(c => c.Name);

            Months = appContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = requestCulture.RequestCulture.Culture.Name == "de" ? c.Name_de :
            requestCulture.RequestCulture.UICulture.Name == "fr" ? c.Name_fr :
            requestCulture.RequestCulture.UICulture.Name == "it" ? c.Name_it :
            requestCulture.RequestCulture.UICulture.Name == "es" ? c.Name_es :
            c.Name
            }).OrderBy(c => c.MonthCode);

            PhonePrefix = !string.IsNullOrEmpty(Person?.CountryCode) ? "+" + Countries.Where(c => c.CountryCode == Person.CountryCode).Select(c => c.PhoneCode).Single().ToString() : string.Empty;

            if (Person.Nationalities == null || Person.Nationalities.Count == 0)
            {
                Person.Nationalities = new List<NationalityVM>() { new NationalityVM() { Order = 1 } };
            }
        }
        #endregion
    }
}