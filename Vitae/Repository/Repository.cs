﻿using Library.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Model.Poco;
using Model.ViewModels;
using Model.ViewModels.Reports;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public void Log(Guid curriculumID, LogArea logArea, LogLevel logLevel, string page, string userAgent, string userLanguage, string ipAddress, string message = null)
        {
            vitaeContext.Logs.Add(new Log()
            {
                CurriculumID = curriculumID,
                LogArea = logArea,
                IpAddress = ipAddress,
                LogLevel = logLevel,
                Page = page,
                UserAgent = userAgent,
                UserLanguage = userLanguage,
                Message = message,
                Timestamp = DateTime.Now,
            });

            vitaeContext.SaveChanges();
        }

        public async Task<Guid> AddCurriculumAsync(Guid userid, string language)
        {
            var curriculum = new Curriculum()
            {
                UserID = userid,
                CreatedOn = DateTime.Now,
                LastUpdated = DateTime.Now,
                Language = language,
                Person = new Person()
            };
            var languagePoco = vitaeContext.Languages.SingleOrDefault(l => l.LanguageCode == language.ToLower()) ?? vitaeContext.Languages.Single(l => l.LanguageCode == Globals.DEFAULT_LANGUAGE);
            curriculum.CurriculumLanguages = new List<CurriculumLanguage>() { new CurriculumLanguage() { Curriculum = curriculum, CurriculumID = curriculum.CurriculumID, Language = languagePoco, LanguageID = languagePoco.LanguageID } };
            vitaeContext.Curriculums.Add(curriculum);

            await vitaeContext.SaveChangesAsync();

            return curriculum.CurriculumID;
        }

        public async Task<Curriculum> GetCurriculumByWeakIdentifierAsync(string identifier)
        {
            Curriculum curriculum = null;
            var curriculumID = vitaeContext.Curriculums.SingleOrDefault(c => c.CurriculumID.ToString().ToLower() == identifier.ToLower())?.CurriculumID;

            if (curriculumID.HasValue)
            {
                curriculum = await GetCurriculumAsync(curriculumID.Value);
            }

            return curriculum;
        }

        public async Task<Curriculum> GetCurriculumAsync(Guid curriculumID)
        {
            var curriculumQuery = vitaeContext.Curriculums
               .Include(c => c.CurriculumLanguages)
               .ThenInclude(cl => cl.Language)
               .Where(c => c.CurriculumID == curriculumID).Take(1);

            await curriculumQuery.Include(c => c.Person).LoadAsync();
            await curriculumQuery.Include(c => c.Person.PersonalDetails).LoadAsync();
            await curriculumQuery.Include(c => c.Person.PersonalDetails).ThenInclude(pd => pd.Children).LoadAsync();
            await curriculumQuery.Include(c => c.Person.PersonalDetails).ThenInclude(pd => pd.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.PersonalDetails).ThenInclude(pd => pd.Language).LoadAsync();
            await curriculumQuery.Include(c => c.Person.PersonalDetails).ThenInclude(pd => pd.PersonCountries).ThenInclude(pc => pc.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Abouts).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Abouts).ThenInclude(a => a.Vfile).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Abroads).ThenInclude(a => a.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Awards).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Courses).ThenInclude(c => c.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Certificates).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Educations).ThenInclude(e => e.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Experiences).ThenInclude(e => e.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Interests).LoadAsync();
            await curriculumQuery.Include(c => c.Person.LanguageSkills).ThenInclude(ls => ls.SpokenLanguage).LoadAsync();
            await curriculumQuery.Include(c => c.Person.Skills).LoadAsync();
            await curriculumQuery.Include(c => c.Person.SocialLinks).LoadAsync();
            await curriculumQuery.Include(c => c.Person.References).ThenInclude(r => r.Country).LoadAsync();

            return curriculumQuery.Single();
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

        public IEnumerable<LanguageVM> GetCurriculumLanguages(Guid curriculumID, string uiCulture)
        {
            var languagesVM = vitaeContext.CurriculumLanguages.Include(cl => cl.Language)
                                    .Where(cl => cl.Curriculum.CurriculumID == curriculumID)
                                    .ToList()
                                    .Select(cl => new LanguageVM
                                    {
                                        LanguageCode = cl.Language.LanguageCode,
                                        Name = uiCulture == "de" ? cl.Language.Name_de :
                                           uiCulture == "fr" ? cl.Language.Name_fr :
                                           uiCulture == "it" ? cl.Language.Name_it :
                                           uiCulture == "es" ? cl.Language.Name_es :
                                        cl.Language.Name
                                    }).OrderBy(l => l.Order);
            return languagesVM;
        }

        public PersonalDetailVM GetPersonalDetail(Curriculum curriculum, string email = null)
        {
            var personalDetail = curriculum.Person.PersonalDetails.SingleOrDefault(pd => pd.Language == curriculum.CurriculumLanguages.Single(cl => cl.Order == 0).Language);

            var personVM = new PersonalDetailVM()
            {
                Birthday_Day = personalDetail?.Birthday.Day ?? 1,
                Birthday_Month = personalDetail?.Birthday.Month ?? 1,
                Birthday_Year = personalDetail?.Birthday.Year ?? DateTime.Now.Year,
                City = personalDetail?.City,
                CountryCode = personalDetail?.Country.CountryCode,
                Citizenship = personalDetail?.Citizenship,
                Email = personalDetail?.Email ?? email,
                Firstname = personalDetail?.Firstname,
                Lastname = personalDetail?.Lastname,
                Gender = personalDetail?.Gender,
                LanguageCode = personalDetail?.Language.LanguageCode,
                MobileNumber = personalDetail?.MobileNumber,
                Street = personalDetail?.Street,
                StreetNo = personalDetail?.StreetNo,
                ZipCode = personalDetail?.ZipCode,
                State = personalDetail?.State,
                PhonePrefix = GetPhonePrefix(personalDetail?.Country.CountryCode),
                MaritalStatus = personalDetail?.MaritalStatus ?? MaritalStatus.NoInformation,
                Children = personalDetail?.Children?.OrderBy(c => c.Order)
                .Select(n => new ChildVM
                {
                    Firstname = n.Firstname,
                    Order = n.Order,
                    Birthday_Year = n.Birthday.Year,
                    Birthday_Month = n.Birthday.Month,
                    Birthday_Day = n.Birthday.Day
                }).ToList() ?? new List<ChildVM>(),
                Nationalities = personalDetail?.PersonCountries?.OrderBy(pc => pc.Order)
                .Select(n => new NationalityVM { CountryCode = n.Country.CountryCode, Order = n.Order })
                .ToList() ?? new List<NationalityVM>() { new NationalityVM() { Order = 0 } }
            };

            return personVM;
        }

        public IList<AboutVM> GetAbouts(Curriculum curriculum)
        {
            var aboutVM = curriculum.Person.Abouts?.OrderBy(a => a.Order)
                .Select(a => new AboutVM()
                {
                    AcademicTitle = a.AcademicTitle,
                    Photo = a.Photo,
                    Slogan = a.Slogan,
                    Vfile = new VfileVM()
                    {
                        FileName = a.Vfile?.FileName,
                        Identifier = a.Vfile?.Identifier ?? Guid.Empty
                    }
                }).ToList();

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
                   Issuer = c.Issuer,
                   NeverExpires = !c.ExpiresOn.HasValue,
                   Order = c.Order
               }).ToList();

            return certificatesVM;
        }

        public IList<CourseVM> GetCourses(Curriculum curriculum)
        {
            var coursesVM = curriculum.Person.Courses?.OrderBy(co => co.Order)
               .Select(c => new CourseVM()
               {
                   Description = c.Description,
                   Start_Day = c.Start.Day,
                   Start_Month = c.Start.Month,
                   Start_Year = c.Start.Year,
                   End_Day = c.End.HasValue ? c.End.Value.Day : DateTime.Now.Day,
                   End_Month = c.End.HasValue ? c.End.Value.Month : DateTime.Now.Month,
                   End_Year = c.End.HasValue ? c.End.Value.Year : DateTime.Now.Year,
                   Link = c.Link,
                   City = c.City,
                   Title = c.Title,
                   CountryCode = c.Country.CountryCode,
                   SchoolName = c.SchoolName,
                   SingleDay = !c.End.HasValue,
                   Order = c.Order
               }).ToList();

            return coursesVM;
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
                        Association = i.Association,
                        Link = i.Link,
                        InterestName = i.InterestName,
                        Order = i.Order
                    }).ToList();

            return interestsVM;
        }

        public IList<LanguageSkillVM> GetLanguageSkills(Curriculum curriculum)
        {
            var languageSkillsVM = curriculum.Person.LanguageSkills?.OrderBy(ls => ls.Order)
                    .Select(l => new LanguageSkillVM()
                    {
                        LanguageCode = l.SpokenLanguage?.LanguageCode,
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
                    Hide = r.Hide,
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

        public IList<LogVM> GetHits(Guid curriculumID, int? days = null)
        {
            var lastHits = vitaeContext.Logs
                .Where(l => l.CurriculumID == curriculumID && l.LogArea == LogArea.Access)
                .Where(l => !days.HasValue || l.Timestamp > DateTime.Now.Date.AddDays(-days.Value))
                .GroupBy(l => l.Timestamp.Date)
                .Select(l => new LogVM()
                {
                    Hits = l.Count(),
                    LogDate = l.Key
                }).ToList();

            return lastHits;
        }

        public IList<LogVM> GetAllHits(Guid curriculumID)
        {
            var lastHits = GetHits(curriculumID);
            var allLogins = new List<LogVM>();
            var currentHits = 0;
            foreach (var hit in lastHits)
            {
                currentHits += hit.Hits;
                allLogins.Add(new LogVM() { Hits = currentHits, LogDate = hit.LogDate });
            }

            return allLogins;
        }

        public IList<LogVM> GetLogins(Guid curriculumID, int? days = null)
        {
            var lastHits = vitaeContext.Logs
                .Where(l => l.CurriculumID == curriculumID && l.LogArea == LogArea.Login)
                .Where(l => !days.HasValue || l.Timestamp > DateTime.Now.Date.AddDays(-days.Value))
                .GroupBy(l => l.Timestamp.Date)
                .Select(l => new LogVM()
                {
                    Hits = l.Count(),
                    LogDate = l.Key
                }).ToList();

            return lastHits;
        }

        #endregion
    }
}