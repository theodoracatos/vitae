using Library.ViewModels;

using Microsoft.EntityFrameworkCore;

using Persistency.Data;
using Persistency.Poco;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Repository
{
    public class Repository
    {
        private readonly VitaeContext vitaeContext;

        public Repository(VitaeContext vitaeContext)
        {
            this.vitaeContext = vitaeContext;
        }

        public Curriculum GetCurriculumByWeakIdentifier(string identifier)
        {
            Curriculum curriculum = null;
            var curriculumID = vitaeContext.Curriculums.SingleOrDefault(c => c.Identifier.ToString().ToLower() == identifier.ToLower() || c.FriendlyId == identifier)?.CurriculumID;

            if (curriculumID.HasValue)
            {
                curriculum = GetCurriculum(curriculumID.Value);
            }

            return curriculum;
        }

        public Curriculum GetCurriculum(Guid curriculumID)
        {
            var curriculum = vitaeContext.Curriculums
            .Include(c => c.Person)
            .Include(c => c.Person.About)
            .Include(c => c.Person.About.Vfile)
            .Include(c => c.Person.Awards)
            .Include(c => c.Person.Country)
            .Include(c => c.Person.Educations).ThenInclude(e => e.Country)
            .Include(c => c.Person.Experiences).ThenInclude(e => e.Country)
            .Include(c => c.Person.Interests)
            .Include(c => c.Person.Language)
            .Include(c => c.Person.LanguageSkills).ThenInclude(ls => ls.Language)
            .Include(c => c.Person.PersonCountries).ThenInclude(pc => pc.Country)
            .Include(c => c.Person.Skills)
            .Include(c => c.Person.SocialLinks)
            .Single(c => c.Identifier == curriculumID);

            return curriculum;
        }

        public IEnumerable<MonthVM> GetMonths(string uiCulture)
        {
            var monthsVM = vitaeContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = uiCulture == "de" ? c.Name_de :
                uiCulture == "fr" ? c.Name_fr :
                uiCulture == "it" ? c.Name_it :
                uiCulture == "es" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);

            return monthsVM;
        }

        public IEnumerable<CountryVM> GetCountries(string uiCulture)
        {
            var countriesVM = vitaeContext.Countries.Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = uiCulture == "de" ? c.Name_de :
                uiCulture == "fr" ? c.Name_fr :
                uiCulture == "it" ? c.Name_it :
                uiCulture == "es" ? c.Name_es :
                c.Name,
                PhoneCode = c.PhoneCode
            }).OrderBy(c => c.Name);

            return countriesVM;
        }

        public IEnumerable<LanguageVM> GetLanguages(string uiCulture)
        {
            var languagesVM = vitaeContext.Languages.Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Name = uiCulture == "de" ? c.Name_de :
                       uiCulture == "fr" ? c.Name_fr :
                       uiCulture == "it" ? c.Name_it :
                       uiCulture == "es" ? c.Name_es :
                        c.Name
            }).OrderBy(c => c.Name);

            return languagesVM;
        }

        public PersonVM GetPerson(Curriculum curriculum, string email = null)
        {
            var personVM = new PersonVM()
            {
                Birthday_Day = curriculum.Person?.Birthday.Value.Day ?? 1,
                Birthday_Month = curriculum.Person?.Birthday.Value.Month ?? 1,
                Birthday_Year = curriculum.Person?.Birthday.Value.Year ?? DateTime.Now.Year - 1,
                City = curriculum.Person?.City,
                CountryCode = curriculum.Person?.Country.CountryCode,
                Email = curriculum.Person?.Email ?? email,
                Firstname = curriculum.Person?.Firstname,
                Lastname = curriculum.Person?.Lastname,
                Gender = curriculum.Person?.Gender,
                LanguageCode = curriculum.Person?.Language.LanguageCode,
                MobileNumber = curriculum.Person?.MobileNumber,
                Street = curriculum.Person?.Street,
                StreetNo = curriculum.Person?.StreetNo,
                ZipCode = curriculum.Person?.ZipCode,
                State = curriculum.Person?.State,
                Nationalities = curriculum.Person?.PersonCountries?.OrderBy(pc => pc.Order)
                .Select(n => new NationalityVM { CountryCode = n.Country.CountryCode, Order = n.Order })
                .ToList() ?? new List<NationalityVM>() { new NationalityVM() { Order = 0 } }
            };

            return personVM;
        }

        public AboutVM GetAbout(Curriculum curriculum)
        {
            var aboutVM = new AboutVM()
            {
                Photo = curriculum.Person.About?.Photo,
                Slogan = curriculum.Person.About?.Slogan,
                Vfile = new VfileVM()
                {
                    FileName = curriculum.Person.About?.Vfile?.FileName,
                    Identifier = curriculum.Person.About?.Vfile?.Identifier ?? Guid.Empty
                }
            };

            return aboutVM;
        }

        public IList<AwardVM> GetAwards(Curriculum curriculum)
        {
            var awardsVM = curriculum.Person.Awards?.OrderBy(aw => aw.Order)
                   .Select(a => new AwardVM()
                   {
                       AwardedFrom = a.AwardedFrom,
                       Description = a.Description,
                       Link = a.Link,
                       Month = a.AwardedOn.Month,
                       Year = a.AwardedOn.Year,
                       Name = a.Name,
                       Order = a.Order
                   }).ToList();

            return awardsVM;
        }

        public IList<EducationVM> GetEducations(Curriculum curriculum)
        {
            var educationsVM = curriculum.Person.Educations?.OrderBy(ed => ed.Order)
                    .Select(e => new EducationVM()
                    {
                        City = e.City,
                        Start_Month = e.Start.Month,
                        Start_Year = e.Start.Year,
                        End_Month = e.End.HasValue ? e.End.Value.Month : DateTime.Now.Month,
                        End_Year = e.End.HasValue ? e.End.Value.Year : DateTime.Now.Year,
                        UntilNow = !e.End.HasValue,
                        Grade = e.Grade,
                        Order = e.Order,
                        Resumee = e.Resumee,
                        Link = e.Link,
                        SchoolName = e.SchoolName,
                        Subject = e.Subject,
                        Title = e.Title,
                        CountryCode = e.Country.CountryCode
                    })
                    .ToList();

            return educationsVM;
        }

        public IList<ExperienceVM> GetExperiences(Curriculum curriculum)
        {
            var experiencesVM = curriculum.Person.Experiences?.OrderBy(ex => ex.Order)
                    .Select(e => new ExperienceVM()
                    {
                        City = e.City,
                        Start_Month = e.Start.Month,
                        Start_Year = e.Start.Year,
                        End_Month = e.End.HasValue ? e.End.Value.Month : DateTime.Now.Month,
                        End_Year = e.End.HasValue ? e.End.Value.Year : DateTime.Now.Year,
                        UntilNow = !e.End.HasValue,
                        Order = e.Order,
                        Resumee = e.Resumee,
                        Link = e.Link,
                        CompanyName = e.CompanyName,
                        JobTitle = e.JobTitle,
                        CountryCode = e.Country.CountryCode
                    })
                    .ToList();

            return experiencesVM;
        }

        public IList<InterestVM> GetInterests(Curriculum curriculum)
        {
            var interestsVM = curriculum.Person.Interests?.OrderBy(ir => ir.Order)
                    .Select(i => new InterestVM()
                    {
                        Description = i.Description,
                        Link = i.Link,
                        Name = i.Name,
                        Order = i.Order
                    }).ToList();

            return interestsVM;
        }

        public IList<LanguageSkillVM> GetLanguageSkills(Curriculum curriculum)
        {
            var languageSkillsVM = curriculum.Person.LanguageSkills?.OrderBy(ls => ls.Order)
                    .Select(l => new LanguageSkillVM()
                    {
                        LanguageCode = l.Language?.LanguageCode,
                        Order = l.Order,
                        Rate = l.Rate
                    }).ToList();

            return languageSkillsVM;
        }

        public IList<SkillVM> GetSkills(Curriculum curriculum)
        {
            var skillsVM = curriculum.Person.Skills?.OrderBy(ls => ls.Order)
                    .Select(s => new SkillVM()
                    {
                        Category = s.Category,
                        Order = s.Order,
                        Skillset = s.Skillset
                    }).ToList();

            return skillsVM;
        }

        public IList<SocialLinkVM> GetSocialLinks(Curriculum curriculum)
        {
            var socialLinksVM = curriculum.Person.SocialLinks?.OrderBy(ls => ls.Order)
                    .Select(s => new SocialLinkVM()
                    {
                        Link = s.Link,
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform
                    }).ToList();

            return socialLinksVM;
        }
    }
}