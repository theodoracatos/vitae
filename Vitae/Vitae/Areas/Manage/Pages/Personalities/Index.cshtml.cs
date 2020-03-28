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

namespace Vitae.Areas.Manage.Pages.Personalities
{
    public class IndexModel : BasePageModel
    {
        public const string PAGE_PERSONALITIES = "_Personalities";

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

        public async Task<IActionResult> OnGetAsync()
        {
            if (curriculumID == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.CurriculumID == curriculumID))
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
                PersonalDetail = repository.GetPersonalDetail(curriculum, identity.Name);

                FillSelectionViewModel();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID);
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
                curriculum.Person.PersonalDetail.MaritalStatus = PersonalDetail.MaritalStatus;
                curriculum.Person.PersonalDetail.Street = PersonalDetail.Street;
                curriculum.Person.PersonalDetail.StreetNo = PersonalDetail.StreetNo;
                curriculum.Person.PersonalDetail.ZipCode = PersonalDetail.ZipCode;
                curriculum.Person.PersonalDetail.State = PersonalDetail.State;
                curriculum.Person.PersonalDetail.Citizenship = PersonalDetail.Citizenship;

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

                // Children
                if (PersonalDetail.Children != null)
                {
                    vitaeContext.RemoveRange(curriculum.Person.PersonalDetail.Children);
                    curriculum.Person.PersonalDetail.Children =
                        PersonalDetail.Children?.Select(c => new Child()
                        {
                            Firstname = c.Firstname,
                            Birthday = new DateTime(c.Birthday_Year, c.Birthday_Month, c.Birthday_Day),
                            Order = c.Order
                        }).ToList();
                }
                curriculum.LastUpdated = DateTime.Now;
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
            Months = repository.GetMonths(requestCulture.RequestCulture.UICulture.Name);
            PersonalDetail.PhonePrefix = repository.GetPhonePrefix(PersonalDetail.CountryCode);
        }
        #endregion
    }
}