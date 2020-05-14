using Library.Constants;
using Library.Helper;
using Library.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Model.Poco;
using Model.ViewModels;
using Model.ViewModels.Reports;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task LogAsync(Guid curriculumID, Guid? publicationID, LogArea logArea, LogLevel logLevel, string link, string userAgent, string userLanguage, string ipAddress, string message = null)
        {
            vitaeContext.Logs.Add(new Log()
            {
                CurriculumID = curriculumID,
                PublicationID = publicationID,
                LogArea = logArea,
                IpAddress = ipAddress,
                LogLevel = logLevel,
                Link = link,
                UserAgent = userAgent,
                UserLanguage = userLanguage,
                Message = message
            });

            await vitaeContext.SaveChangesAsync();
        }

        public async Task<Guid> AddCurriculumAsync(Guid userid, string language)
        {
            var languagePoco = vitaeContext.Languages.SingleOrDefault(l => l.LanguageCode == language.ToLower()) ?? vitaeContext.Languages.Single(l => l.LanguageCode == Globals.DEFAULT_LANGUAGE);

            var curriculum = new Curriculum()
            {
                UserID = userid,
                LastUpdated = DateTime.Now,
                Language = languagePoco
            };
            curriculum.CurriculumLanguages = new List<CurriculumLanguage>() { new CurriculumLanguage() { Curriculum = curriculum, CurriculumID = curriculum.CurriculumID, Language = languagePoco, LanguageID = languagePoco.LanguageID } };
            vitaeContext.Curriculums.Add(curriculum);

            await vitaeContext.SaveChangesAsync();

            return curriculum.CurriculumID;
        }

        public async Task<Dictionary<string, int>> CountItemsFromCurriculumLanguageAsync(Guid curriculumID, string languageToCheck)
        {
            var categoryCount = new Dictionary<string, int>();
            var curriculum = await GetCurriculumAsync(curriculumID);

            categoryCount.Add(SharedResource.PersonalDetails, curriculum.PersonalDetails.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.About, curriculum.Abouts.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Abroads, curriculum.Abroads.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Awards, curriculum.Awards.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Courses, curriculum.Courses.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Certificates, curriculum.Certificates.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Educations, curriculum.Educations.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Experiences, curriculum.Experiences.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Interests, curriculum.Interests.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Languages, curriculum.LanguageSkills.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.Skills, curriculum.Skills.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.SocialLinks, curriculum.SocialLinks.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));
            categoryCount.Add(SharedResource.References, curriculum.References.Count(a => languageToCheck == null || a.CurriculumLanguage.LanguageCode == languageToCheck));

            return categoryCount;
        }

        public async Task DeleteItemsFromCurriculumLanguageAsync(Guid curriculumID, string languageCodeToDelete)
        {
            var curriculum = await GetCurriculumAsync(curriculumID);

            curriculum.Abouts.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Abouts.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete && a.Vfile != null).ToList().ForEach(i => vitaeContext.Entry(i.Vfile).State = EntityState.Deleted);
            curriculum.Abroads.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Awards.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Courses.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Certificates.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Educations.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Experiences.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Interests.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.LanguageSkills.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.Skills.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.SocialLinks.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);
            curriculum.References.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeToDelete).ToList().ForEach(i => vitaeContext.Entry(i).State = EntityState.Deleted);

            await vitaeContext.SaveChangesAsync();
        }

        public async Task MoveItemsFromCurriculumLanguageAsync(Guid curriculumID, string languageCodeFrom, string languageCodeTo, bool copy = false)
        {
            var curriculum = await GetCurriculumAsync(curriculumID);
            var newLanguage = vitaeContext.Languages.Single(l => l.LanguageCode == languageCodeTo);

            curriculum.PersonalDetails.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                 .ToList().ForEach(a => curriculum.PersonalDetails.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Abouts.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Abouts.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Abouts.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
            .ToList().ForEach(a => curriculum.Abouts.Single(ab => ab.CurriculumLanguage.LanguageCode == newLanguage.LanguageCode).Vfile = (copy ? CopyEntity(a.Vfile) : a.Vfile));

            curriculum.Abroads.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Abroads.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Awards.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Awards.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Courses.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Courses.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Certificates.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Certificates.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Educations.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Educations.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Experiences.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom).ToList()
                .ForEach(a => curriculum.Experiences.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Interests.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Interests.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.LanguageSkills.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.LanguageSkills.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.Skills.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.Skills.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.SocialLinks.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.SocialLinks.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            curriculum.References.Where(a => a.CurriculumLanguage.LanguageCode == languageCodeFrom)
                .ToList().ForEach(a => curriculum.References.Add(SetKeys(copy ? CopyEntity(a) : a, newLanguage)));

            await vitaeContext.SaveChangesAsync();
        }

        public async Task RemoveCurriculumLanguageItemsAsync(Guid curriculumID, string languageCode)
        {
            var curriculum = await GetCurriculumAsync(curriculumID);
            bool removeQuery(Base x) => x.CurriculumLanguage.LanguageCode == languageCode;

            vitaeContext.RemoveRange(curriculum.PersonalDetails.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Abouts.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Abroads.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Awards.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Courses.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Certificates.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Educations.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Experiences.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Interests.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.LanguageSkills.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.Skills.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.SocialLinks.Where((Func<Base, bool>)removeQuery));
            vitaeContext.RemoveRange(curriculum.References.Where((Func<Base, bool>)removeQuery));

            await vitaeContext.SaveChangesAsync();
        }

        public async Task<Curriculum> GetCurriculumAsync(Guid curriculumID)
        {
            var curriculumQuery = vitaeContext.Curriculums
                .Include(c => c.CurriculumLanguages)
                .ThenInclude(cl => cl.Language)
                .Where(c => c.CurriculumID == curriculumID).Take(1);

            await LoadPersonalDetails(curriculumQuery);
            await LoadAbouts(curriculumQuery);
            await LoadAbroads(curriculumQuery);
            await LoadAwards(curriculumQuery);
            await LoadCourses(curriculumQuery);
            await LoadCertificates(curriculumQuery);
            await LoadEducations(curriculumQuery);
            await LoadExperiences(curriculumQuery);
            await LoadInterests(curriculumQuery);
            await LoadLanguageSkills(curriculumQuery);
            await LoadSkills(curriculumQuery);
            await LoadSocialLinks(curriculumQuery);
            await LoadReferences(curriculumQuery);
            await LoadPublications(curriculumQuery);

            return curriculumQuery.Single();
        }
        
        public async Task<Curriculum> GetCurriculumAsync<T>(Guid curriculumID) where T : Base
        {
            var curriculumQuery = vitaeContext.Curriculums
               .Include(c => c.CurriculumLanguages)
               .ThenInclude(cl => cl.Language)
               .Where(c => c.CurriculumID == curriculumID).Take(1);

            if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(PersonalDetail)))
            {
                await LoadPersonalDetails(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(About)))
            {
                await LoadAbouts(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Abroad)))
            {
                await LoadAbroads(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Award)))
            {
                await LoadAwards(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Course)))
            {
                await LoadCourses(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Certificate)))
            {
                await LoadCertificates(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Education)))
            {
                await LoadEducations(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Experience)))
            {
                await LoadExperiences(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Interest)))
            {
                await LoadInterests(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(LanguageSkill)))
            {
                await LoadLanguageSkills(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Skill)))
            {
                await LoadSkills(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(SocialLink)))
            {
                await LoadSocialLinks(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Reference)))
            {
                await LoadReferences(curriculumQuery);
            }
            else if (CodeHelper.IsSubclassOfRawGeneric(typeof(T), typeof(Publication)))
            {
                await LoadPublications(curriculumQuery);
            }

            return curriculumQuery.Single();
        }

        public IEnumerable<MonthVM> GetMonths(string uiCulture)
        {
            var monthsVM = vitaeContext.Months.Select(c => new MonthVM()
            {
                MonthCode = c.MonthCode,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? c.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? c.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? c.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? c.Name_es :
                c.Name
            }).OrderBy(c => c.MonthCode);

            return monthsVM;
        }

        public IEnumerable<CountryVM> GetCountries(string uiCulture)
        {
            var countriesVM = vitaeContext.Countries.Select(c => new CountryVM()
            {
                CountryCode = c.CountryCode,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? c.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? c.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? c.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? c.Name_es :
                c.Name,
                PhoneCode = c.PhoneCode,
                Order = c.Order
            }).OrderByDescending(c => c.Order).ThenBy(c => c.Name);

            return countriesVM;
        }

        public IEnumerable<IndustryVM> GetIndustries(string uiCulture)
        {
            var industriesVM = vitaeContext.Industries.Select(i => new IndustryVM()
            {
                IndustryCode = i.IndustryCode,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? i.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? i.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? i.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? i.Name_es :
                i.Name
            }).OrderBy(i => i.Name);

            return industriesVM;
        }

        public IEnumerable<HierarchyLevelVM> GetHierarchyLevels(string uiCulture)
        {
            var hierarchyLevelsVM = vitaeContext.HierarchyLevels.Select(h => new HierarchyLevelVM()
            {
                HierarchyLevelCode = h.HierarchyLevelCode,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? h.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? h.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? h.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? h.Name_es :
                h.Name
            }).OrderBy(i => i.HierarchyLevelCode);

            return hierarchyLevelsVM;
        }

        public IEnumerable<MaritalStatusVM> GetMaritalStatuses(string uiCulture)
        {
            var maritalStatusesVM = vitaeContext.MaritalStatuses.Select(m => new MaritalStatusVM()
            {
                MaritalStatusCode = m.MaritalStatusCode,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? m.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? m.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? m.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? m.Name_es :
                m.Name,
            }).OrderBy(m => m.Name);

            return maritalStatusesVM;
        }

        public IEnumerable<LanguageVM> GetLanguages(string uiCulture)
        {
            var languagesVM = vitaeContext.Languages.Select(c => new LanguageVM()
            {
                LanguageCode = c.LanguageCode,
                Order = c.Order,
                Name = uiCulture == $"{ApplicationLanguage.de}" ? c.Name_de :
                uiCulture == $"{ApplicationLanguage.fr}" ? c.Name_fr :
                uiCulture == $"{ApplicationLanguage.it}" ? c.Name_it :
                uiCulture == $"{ApplicationLanguage.es}" ? c.Name_es :
                 c.Name,
            }).OrderByDescending(c => c.Order).ThenBy(c => c.Name);

            return languagesVM;
        }

        public IEnumerable<CurriculumLanguageVM> GetCurriculumLanguages(Guid curriculumID, string uiCulture)
        {
            var languagesVM = vitaeContext.CurriculumLanguages.Include(cl => cl.Language)
                                    .Where(cl => cl.Curriculum.CurriculumID == curriculumID)
                                    .ToList()
                                    .Select(cl => new CurriculumLanguageVM
                                    {
                                        LanguageCode = cl.Language.LanguageCode,
                                        Name = uiCulture == $"{ApplicationLanguage.de}" ? cl.Language.Name_de :
                                           uiCulture == $"{ApplicationLanguage.fr}" ? cl.Language.Name_fr :
                                           uiCulture == $"{ApplicationLanguage.it}" ? cl.Language.Name_it :
                                           uiCulture == $"{ApplicationLanguage.es}" ? cl.Language.Name_es :
                                        cl.Language.Name,
                                        Order = cl.Order,
                                        IsSelected = cl.IsSelected
                                    }).OrderBy(l => l.Order);
            return languagesVM;
        }

        public PersonalDetailVM GetPersonalDetail(Curriculum curriculum, string email = null)
        {
            var personalDetail = curriculum.PersonalDetails.SingleOrDefault(pd => pd.CurriculumLanguage == curriculum.CurriculumLanguages.Single(cl => cl.Order == 0).Language);

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
                LanguageCode = personalDetail?.SpokenLanguage.LanguageCode,
                PhonePrefix = personalDetail?.PhonePrefix,
                MobileNumber = personalDetail?.MobileNumber,
                Street = personalDetail?.Street,
                StreetNo = personalDetail?.StreetNo,
                ZipCode = personalDetail?.ZipCode,
                State = personalDetail?.State,
                MaritalStatusCode = personalDetail?.MaritalStatus.MaritalStatusCode ?? 1,
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

        public IList<AboutVM> GetAbouts(Curriculum curriculum, string languageCode)
        {
            var aboutsVM = curriculum.Abouts
                .Where(a => a.CurriculumLanguage.LanguageCode == languageCode)
               .OrderBy(aw => aw.Order)
               .Select(a => new AboutVM()
               {
                   AcademicTitle = a.AcademicTitle,
                   Photo = a.Photo,
                   Slogan = a.Slogan,
                   Vfile = new VfileVM()
                   {
                       FileName = a.Vfile?.FileName,
                       Identifier = a.Vfile?.VfileID ?? Guid.Empty
                   }
               }).ToList();

            return aboutsVM;
        }

        public IList<AwardVM> GetAwards(Curriculum curriculum, string languageCode)
        {
            var awardsVM = curriculum.Awards?
                .Where(a => a.CurriculumLanguage.LanguageCode == languageCode)
                .OrderBy(aw => aw.Order)
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

        public IList<CertificateVM> GetCertificates(Curriculum curriculum, string languageCode)
        {
            var certificatesVM = curriculum.Certificates?
                .Where(a => a.CurriculumLanguage.LanguageCode == languageCode)
                .OrderBy(ce => ce.Order)
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

        public IList<CourseVM> GetCourses(Curriculum curriculum, string languageCode)
        {
            var coursesVM = curriculum.Courses?
               .Where(c => c.CurriculumLanguage?.LanguageCode == languageCode)
               .OrderBy(co => co.Order)
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

        public IList<EducationVM> GetEducations(Curriculum curriculum, string languageCode)
        {
            var educationsVM = curriculum.Educations?
                 .Where(e => e.CurriculumLanguage?.LanguageCode == languageCode)
                 .OrderBy(ed => ed.Order)
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

        public IList<ExperienceVM> GetExperiences(Curriculum curriculum, string languageCode)
        {
            var experiencesVM = curriculum.Experiences?
                .Where(e => e.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(ex => ex.Order)
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
                        CompanyDescription = e.CompanyDescription,
                        JobTitle = e.JobTitle,
                        CountryCode = e.Country.CountryCode,
                        IndustryCode= e.Industry.IndustryCode,
                        HierarchyLevelCode = e.HierarchyLevel.HierarchyLevelCode
                    })
                    .ToList();

            return experiencesVM;
        }

        public IList<InterestVM> GetInterests(Curriculum curriculum, string languageCode)
        {
            var interestsVM = curriculum.Interests?
                .Where(i => i.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(ir => ir.Order)
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

        public IList<LanguageSkillVM> GetLanguageSkills(Curriculum curriculum, string languageCode)
        {
            var languageSkillsVM = curriculum.LanguageSkills?
                    .Where(l => l.CurriculumLanguage?.LanguageCode == languageCode)
                    .OrderBy(la => la.Order)
                    .Select(l => new LanguageSkillVM()
                    {
                        LanguageCode = l.SpokenLanguage?.LanguageCode,
                        Order = l.Order,
                        Rate = l.Rate
                    }).ToList();

            return languageSkillsVM;
        }

        public IList<SkillVM> GetSkills(Curriculum curriculum, string languageCode)
        {
            var skillsVM = curriculum.Skills?
                .Where(l => l.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(ls => ls.Order)
                    .Select(s => new SkillVM()
                    {
                        Category = s.Category,
                        Order = s.Order,
                        Skillset = s.Skillset
                    }).ToList();

            return skillsVM;
        }

        public IList<SocialLinkVM> GetSocialLinks(Curriculum curriculum, string languageCode)
        {
            var socialLinksVM = curriculum.SocialLinks?
                .Where(l => l.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(ls => ls.Order)
                    .Select(s => new SocialLinkVM()
                    {
                        Link = s.Link,
                        Order = s.Order,
                        SocialPlatform = s.SocialPlatform
                    }).ToList();

            return socialLinksVM;
        }

        public IList<ReferenceVM> GetReferences(Curriculum curriculum, string languageCode)
        {
            var referencesVM = curriculum.References?
                .Where(r => r.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(re => re.Order)
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
                    PhonePrefix = r.PhonePrefix,
                    PhoneNumber = r.PhoneNumber
                }).ToList();

            return referencesVM;
        }

        public IList<AbroadVM> GetAbroads(Curriculum curriculum, string languageCode)
        {
            var abroadsVM = curriculum.Abroads?
                .Where(e => e.CurriculumLanguage?.LanguageCode == languageCode)
                .OrderBy(ab => ab.Order)
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

        public IList<PublicationVM> GetPublications(Curriculum curriculum, string baseUrl)
        {
            var publicationsVM = curriculum.Publications?
                .OrderBy(ab => ab.Order)
                .Select(p => new PublicationVM()
                {
                    Order = p.Order,
                    Anonymize = p.Anonymize,
                    EnablePassword = !string.IsNullOrEmpty(p.Password),
                    LanguageCode = p.CurriculumLanguage.LanguageCode,
                    Password = string.IsNullOrEmpty(p.Password) ? null : AesHandler.Decrypt(p.Password, p.PublicationIdentifier.ToString()),
                    PublicationIdentifier = p.PublicationIdentifier.ToString(),
                    Link = $"{baseUrl}/CV/{p.PublicationIdentifier}?culture={p.CurriculumLanguage.LanguageCode.ToLower()}",
                    Notes = p.Notes,
                    QrCode = CodeHelper.CreateQRCode($"{baseUrl}/CV/{p.PublicationIdentifier}")
                }).ToList();

            return publicationsVM;
        }

        public string GetPhonePrefix(string countryCode)
        {
            return !string.IsNullOrEmpty(countryCode) ? "+" + vitaeContext.Countries.Where(c => c.CountryCode == countryCode).Select(c => c.PhoneCode).Single().ToString() : string.Empty;
        }

        public IList<LogVM> GetHits(Guid curriculumID, int? days = null, int hits = 10000)
        {
            var lastHits = vitaeContext.Logs
                .Where(l => l.CurriculumID == curriculumID && l.LogArea == LogArea.Access)
                .Where(l => !days.HasValue || l.Timestamp > DateTime.Now.Date.AddDays(-days.Value))
                .OrderByDescending(l => l.Timestamp)
                .Take(hits)
                .GroupBy(l => new { l.Timestamp.Date, l.PublicationID } )
                .Select(l => new LogVM()
                {
                    Hits = l.Count(),
                    LogDate = l.Key.Date,
                    PublicationID = l.Key.PublicationID.Value
                }).ToList();

            return lastHits;
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

        public Vfile GetFile(Guid identifier)
        {
            var vfile = vitaeContext.Vfiles.SingleOrDefault(v => v.VfileID == identifier);

            return vfile;
        }

        #endregion

        #region Helper

        private T CopyEntity<T>(T basePoco) where T : class, new()
        {
            if(basePoco == null) { return null; }
            var newPoco = CodeHelper.ShallowCopyEntity(basePoco);

            return newPoco;
        }

        private T SetKeys<T>(T basePoco, Language language) where T : Base
        {
            basePoco.CurriculumLanguage = language;

            return basePoco;
        }

        private async Task LoadPersonalDetails(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.PersonalDetails).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.Children).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.Country).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.SpokenLanguage).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.CurriculumLanguage).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.PersonCountries).ThenInclude(pc => pc.Country).LoadAsync();
            await curriculumQuery.Include(c => c.PersonalDetails).ThenInclude(pd => pd.MaritalStatus).LoadAsync();
        }

        private async Task LoadAbouts(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Abouts).ThenInclude(a => a.Vfile).LoadAsync();
        }

        private async Task LoadAbroads(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Abroads).ThenInclude(a => a.Country).LoadAsync();
        }

        private async Task LoadAwards(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Awards).LoadAsync();
        }

        private async Task LoadCourses(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Courses).ThenInclude(c => c.Country).LoadAsync();
        }

        private async Task LoadCertificates(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Certificates).LoadAsync();
        }

        private async Task LoadEducations(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Educations).ThenInclude(e => e.Country).LoadAsync();
        }

        private async Task LoadExperiences(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Experiences).ThenInclude(e => e.Country).LoadAsync();
            await curriculumQuery.Include(c => c.Experiences).ThenInclude(e => e.Industry).LoadAsync();
            await curriculumQuery.Include(c => c.Experiences).ThenInclude(e => e.HierarchyLevel).LoadAsync();
        }

        private async Task LoadInterests(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Interests).LoadAsync();
        }

        private async Task LoadLanguageSkills(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.LanguageSkills).ThenInclude(ls => ls.SpokenLanguage).LoadAsync();
        }

        private async Task LoadSkills(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Skills).LoadAsync();
        }

        private async Task LoadSocialLinks(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.SocialLinks).LoadAsync();
        }

        private async Task LoadReferences(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.References).ThenInclude(r => r.Country).LoadAsync();
        }

        private async Task LoadPublications(IQueryable<Curriculum> curriculumQuery)
        {
            await curriculumQuery.Include(c => c.Publications).LoadAsync();
        }

        #endregion
    }
}