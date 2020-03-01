﻿using Microsoft.EntityFrameworkCore;

using Model.Poco;
using Model.ViewModels;

using Persistency.Data;

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

        #region API

        public void Log()
        {
            vitaeContext.Logs.Add(new Log()
            {
            });
        }

        public Curriculum GetCurriculumByWeakIdentifier(string identifier)
        {
            Curriculum curriculum = null;
            var curriculumID = vitaeContext.Curriculums.SingleOrDefault(c => c.Identifier.ToString().ToLower() == identifier.ToLower() || c.FriendlyId == identifier)?.Identifier;

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
            .Include(c => c.Person.PersonalDetail)
            .Include(c => c.Person.PersonalDetail.Children)
            .Include(c => c.Person.PersonalDetail.Country)
            .Include(c => c.Person.PersonalDetail.Language)
            .Include(c => c.Person.PersonalDetail.PersonCountries).ThenInclude(pc => pc.Country)
            .Include(c => c.Person.About)
            .Include(c => c.Person.About.Vfile)
            .Include(c => c.Person.Abroads).ThenInclude(a => a.Country)
            .Include(c => c.Person.Awards)
            .Include(c => c.Person.Certificates)
            .Include(c => c.Person.Educations).ThenInclude(e => e.Country)
            .Include(c => c.Person.Experiences).ThenInclude(e => e.Country)
            .Include(c => c.Person.Interests)
            .Include(c => c.Person.LanguageSkills).ThenInclude(ls => ls.Language)
            .Include(c => c.Person.Skills)
            .Include(c => c.Person.SocialLinks)
            .Include(c => c.Person.References).ThenInclude(r => r.Country)
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

        public PersonalDetailVM GetPersonalDetail(Curriculum curriculum, string email = null)
        {
            var personVM = new PersonalDetailVM()
            {
                Birthday_Day = curriculum.Person.PersonalDetail?.Birthday.Day ?? 1,
                Birthday_Month = curriculum.Person.PersonalDetail?.Birthday.Month ?? 1,
                Birthday_Year = curriculum.Person.PersonalDetail?.Birthday.Year ?? DateTime.Now.Year,
                City = curriculum.Person.PersonalDetail?.City,
                CountryCode = curriculum.Person.PersonalDetail?.Country.CountryCode,
                Citizenship = curriculum.Person.PersonalDetail?.Citizenship,
                Email = curriculum.Person.PersonalDetail?.Email ?? email,
                Firstname = curriculum.Person.PersonalDetail?.Firstname,
                Lastname = curriculum.Person.PersonalDetail?.Lastname,
                Gender = curriculum.Person.PersonalDetail?.Gender,
                LanguageCode = curriculum.Person.PersonalDetail?.Language.LanguageCode,
                MobileNumber = curriculum.Person.PersonalDetail?.MobileNumber,
                Street = curriculum.Person.PersonalDetail?.Street,
                StreetNo = curriculum.Person.PersonalDetail?.StreetNo,
                ZipCode = curriculum.Person.PersonalDetail?.ZipCode,
                State = curriculum.Person.PersonalDetail?.State,
                PhonePrefix = GetPhonePrefix(curriculum.Person.PersonalDetail?.Country.CountryCode),
                Children = curriculum.Person.PersonalDetail?.Children?.OrderBy(c => c.Order)
                .Select(n => new ChildVM
                {
                    Firstname = n.Firstname,
                    Order = n.Order,
                    Birthday_Year = n.Birthday.Year,
                    Birthday_Month = n.Birthday.Month
                }).ToList() ?? new List<ChildVM>(),
                Nationalities = curriculum.Person.PersonalDetail?.PersonCountries?.OrderBy(pc => pc.Order)
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

        public IList<CertificateVM> GetCertificates(Curriculum curriculum)
        {
            var certificatesVM = curriculum.Person.Certificates?.OrderBy(ce => ce.Order)
               .Select(c => new CertificateVM()
               {
                   Description = c.Description,
                   Start_Month = c.IssuedOn.Month,
                   Start_Year = c.IssuedOn.Year,
                   End_Month = c.ExpiresOn.HasValue ? c.ExpiresOn.Value.Month : DateTime.Now.Month,
                   End_Year = c.ExpiresOn.HasValue ? c.ExpiresOn.Value.Year : DateTime.Now.Year,
                   Link = c.Link,
                   Name = c.Name,
                   NeverExpires = !c.ExpiresOn.HasValue,
                   Order = c.Order
               }).ToList();

            return certificatesVM;
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
                        Description = e.Description,
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
                        Description = e.Description,
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

        public IList<ReferenceVM> GetReferences(Curriculum curriculum)
        {
            var referencesVM = curriculum.Person.References?.OrderBy(re => re.Order)
                .Select(r => new ReferenceVM()
                {
                    CompanyName = r.CompanyName,
                    CountryCode = r.Country.CountryCode,
                    Description = r.Description,
                    Email = r.Email,
                    Firstname = r.Firstname,
                    Lastname = r.Lastname,
                    Gender = r.Gender,
                    Order = r.Order,
                    Link = r.Link,
                    PhoneNumber = r.PhoneNumber
                }).ToList();

            return referencesVM;
        }

        public IList<AbroadVM> GetAbroads(Curriculum curriculum)
        {
            var abroadsVM = curriculum.Person.Abroads?.OrderBy(ab => ab.Order)
                .Select(a => new AbroadVM()
                {
                    City = a.City,
                    CountryCode = a.Country.CountryCode,
                    Description = a.Description,
                    Start_Month = a.Start.Month,
                    Start_Year = a.Start.Year,
                    End_Month = a.End.HasValue ? a.End.Value.Month : DateTime.Now.Month,
                    End_Year = a.End.HasValue ? a.End.Value.Year : DateTime.Now.Year,
                    UntilNow = !a.End.HasValue,
                    Order = a.Order
                }).ToList();

            return abroadsVM;
        }

        public string GetPhonePrefix(string countryCode)
        {
            return !string.IsNullOrEmpty(countryCode) ? "+" + vitaeContext.Countries.Where(c => c.CountryCode == countryCode).Select(c => c.PhoneCode).Single().ToString() : string.Empty;
        }

        #endregion
    }
}