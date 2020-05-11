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
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;
using Vitae.Code.PageModels;

namespace Vitae.Areas.Manage.Pages.Personalities
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_PERSONALITIES = "_Personalities";

        [BindProperty]
        public PersonalDetailVM PersonalDetail { get; set; }

        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<LanguageVM> Languages { get; set; }
        public IEnumerable<MaritalStatusVM> MaritalStatuses { get; set; }

        public IEnumerable<CountryVM> Nationalities { get; set; }
        public IEnumerable<MonthVM> Months { get; set; }

        public int MaxChildren { get; } = 10;

        public int MaxNationalities { get; } = 3;

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
                var curriculum = await repository.GetCurriculumAsync<PersonalDetail>(curriculumID);
                PersonalDetail = repository.GetPersonalDetail(curriculum, identity.Name);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync<PersonalDetail>(curriculumID);
                var curriculumLanguage = curriculum.CurriculumLanguages.Single(cl => cl.Order == 0).Language; // Always take 1st language!
                vitaeContext.RemoveRange(curriculum.PersonalDetails.Where(p => p.CurriculumLanguage.LanguageCode == curriculumLanguage.LanguageCode));

                // Add new
                var personalDetail = new PersonalDetail() { PersonCountries = new List<PersonCountry>(), Children = new List<Child>() };
                personalDetail.Birthday = new DateTime(PersonalDetail.Birthday_Year, PersonalDetail.Birthday_Month, PersonalDetail.Birthday_Day);
                personalDetail.City = PersonalDetail.City;
                personalDetail.Country = vitaeContext.Countries.Single(c => c.CountryCode == PersonalDetail.CountryCode);
                personalDetail.Email = PersonalDetail.Email;
                personalDetail.Firstname = PersonalDetail.Firstname;
                personalDetail.Lastname = PersonalDetail.Lastname;
                personalDetail.Gender = PersonalDetail.Gender.Value;
                personalDetail.SpokenLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == PersonalDetail.LanguageCode);
                personalDetail.PhonePrefix = PersonalDetail.PhonePrefix;
                personalDetail.MobileNumber = PersonalDetail.MobileNumber;
                personalDetail.MaritalStatus = vitaeContext.MaritalStatuses.Single(m => m.MaritalStatusCode == PersonalDetail.MaritalStatusCode);
                personalDetail.Street = PersonalDetail.Street;
                personalDetail.StreetNo = PersonalDetail.StreetNo;
                personalDetail.ZipCode = PersonalDetail.ZipCode;
                personalDetail.State = PersonalDetail.State;
                personalDetail.Citizenship = PersonalDetail.Citizenship;
                personalDetail.CurriculumLanguage = curriculumLanguage;

                // Nationality
                personalDetail.PersonCountries.Clear();
                foreach (var nationality in PersonalDetail.Nationalities)
                {
                    var personCountry = new PersonCountry()
                    {
                        Country = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode),
                        CountryID = vitaeContext.Countries.Single(c => c.CountryCode == nationality.CountryCode).CountryID,
                        PersonalDetail = personalDetail,
                        PersonalDetailID = personalDetail.PersonalDetailID,
                        Order = PersonalDetail.Nationalities.IndexOf(nationality)
                    };
                    personalDetail.PersonCountries.Add(personCountry);
                }

                // Children
                if (PersonalDetail.Children != null)
                {
                    vitaeContext.RemoveRange(personalDetail.Children);
                    personalDetail.Children =
                        PersonalDetail.Children?.Select(c => new Child()
                        {
                            Firstname = c.Firstname,
                            Birthday = new DateTime(c.Birthday_Year, c.Birthday_Month, c.Birthday_Day),
                            Order = c.Order
                        }).ToList();
                }
                curriculum.LastUpdated = DateTime.Now;
                curriculum.PersonalDetails.Add(personalDetail);

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

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostAddNationality()
        {
            if (PersonalDetail.Nationalities.Count < MaxNationalities)
            {
                PersonalDetail.Nationalities.Add(new NationalityVM() { Order = PersonalDetail.Nationalities.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostRemoveNationality()
        {
            Remove(PersonalDetail.Nationalities);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostAddChild()
        {
            if (PersonalDetail.Children == null)
            {
                PersonalDetail.Children = new List<ChildVM>();
            }
            if (PersonalDetail.Children.Count < MaxChildren)
            {
                PersonalDetail.Children.Add(new ChildVM() { Order = PersonalDetail.Children.Count });
            }
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostRemoveChild()
        {
            Remove(PersonalDetail.Children);

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostChangeBirthday()
        {
            PersonalDetail.Birthday_Day = CorrectDate(PersonalDetail.Birthday_Year, PersonalDetail.Birthday_Month, PersonalDetail.Birthday_Day);

            if (PersonalDetail.Children != null)
            {
                foreach (var child in PersonalDetail.Children)
                {
                    child.Birthday_Day = CorrectDate(child.Birthday_Year, child.Birthday_Month, child.Birthday_Day);
                }
            }

            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        public IActionResult OnPostChangeCountry()
        {
            PersonalDetail.PhonePrefix = repository.GetPhonePrefix(PersonalDetail.CountryCode);
            FillSelectionViewModel();

            return GetPartialViewResult(PAGE_PERSONALITIES);
        }

        #endregion

        #region Helper

        protected override void FillSelectionViewModel()
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            Nationalities = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            MaritalStatuses = repository.GetMaritalStatuses(requestCulture.RequestCulture.UICulture.Name);
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            CurriculumLanguages = repository.GetCurriculumLanguages(curriculumID, requestCulture.RequestCulture.UICulture.Name);
        }
        #endregion
    }
}