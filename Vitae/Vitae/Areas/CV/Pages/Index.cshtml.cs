using Library.Helper;
using Library.Repository;
using Library.Resources;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

using Model.Enumerations;
using Model.Poco;
using Model.ViewModels;

using Persistency.Data;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Vitae.Code;

namespace Vitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : BasePageModel
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private string language;

        public Guid CurriculumID { get { return curriculumID; } }

        public PersonalDetailVM PersonalDetail { get; set; }
        
        public IList<AboutVM> Abouts { get; set; }
        public IList<AwardVM> Awards { get; set; } = new List<AwardVM>();
        public IList<EducationVM> Educations { get; set; } = new List<EducationVM>();
        public IList<ExperienceVM> Experiences { get; set; } = new List<ExperienceVM>();
        public IList<CourseVM> Courses { get; set; } = new List<CourseVM>();
        public IList<AbroadVM> Abroads { get; set; } = new List<AbroadVM>();
        public IList<InterestVM> Interests { get; set; } = new List<InterestVM>();
        public IList<LanguageSkillVM> LanguageSkills { get; set; } = new List<LanguageSkillVM>();
        public IList<SkillVM> Skills { get; set; } = new List<SkillVM>();
        public IList<SocialLinkVM> SocialLinks { get; set; } = new List<SocialLinkVM>();
        public IList<CertificateVM> Certificates { get; set; } = new List<CertificateVM>();
        public IList<ReferenceVM> References { get; set; } = new List<ReferenceVM>();

        public IEnumerable<LanguageVM> Languages { get; set; }
        public IEnumerable<CountryVM> Countries { get; set; }
        public IEnumerable<MaritalStatusVM> MaritalStatuses { get; set; }
        public IEnumerable<IndustryVM> Industries { get; set; }
        public IEnumerable<HierarchyLevelVM> HierarchyLevels { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) 
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id, string lang)
        {
            var curriculumID = GetCurriculumID(id, ref lang);

            if (curriculumID == Guid.Empty)
            {
                return NotFound();
            }
            else
            {
                var curriculum = await repository.GetCurriculumAsync(curriculumID.Value);
                language = string.IsNullOrEmpty(lang) ? curriculum.CurriculumLanguages.OrderBy(c => c.Order).First().Language.LanguageCode : lang;

                PersonalDetail = repository.GetPersonalDetail(curriculum);
                Abouts = repository.GetAbouts(curriculum, language);
                SocialLinks = repository.GetSocialLinks(curriculum, language);
                Educations = repository.GetEducations(curriculum, language);
                Experiences = repository.GetExperiences(curriculum, language);
                Courses = repository.GetCourses(curriculum, language);
                Abroads = repository.GetAbroads(curriculum, language);
                LanguageSkills = repository.GetLanguageSkills(curriculum, language);
                Interests = repository.GetInterests(curriculum, language);
                Awards = repository.GetAwards(curriculum, language);
                Skills = repository.GetSkills(curriculum, language);
                Certificates = repository.GetCertificates(curriculum, language);
                References = repository.GetReferences(curriculum, language);

                FillSelectionViewModel();

                if (id.HasValue)
                {
                   await repository.LogAsync(curriculum.CurriculumID, LogArea.Access, LogLevel.Information, CodeHelper.GetCalledUri(httpContext), CodeHelper.GetUserAgent(httpContext), requestCulture.RequestCulture.UICulture.Name, httpContext.Connection.RemoteIpAddress.ToString());
                }
                return Page();
            }
        }

        public IActionResult OnGetOpenFile(Guid identifier)
        {
            var vfile = repository.GetFile(identifier);

            if(vfile != null)
            {
                return File(vfile.Content, vfile.MimeType, vfile.FileName);
            }
            else
            {
                throw new FileNotFoundException(identifier.ToString());
            }
        }

        #region Helper

        protected override void FillSelectionViewModel() 
        {
            Languages = repository.GetLanguages(language);
            Countries = repository.GetCountries(language);
            MaritalStatuses = repository.GetMaritalStatuses(language);
            Industries = repository.GetIndustries(language);
            HierarchyLevels = repository.GetHierarchyLevels(language);
        }

        private Guid? GetCurriculumID(Guid? id, ref string lang)
        {
            Guid? curriculumID = Guid.Empty;

            if (id.HasValue)
            {
                var publication = vitaeContext.Publications.Include(p => p.Curriculum).Include(p => p.CurriculumLanguage).SingleOrDefault(p => p.PublicationIdentifier == id);
                curriculumID = publication?.Curriculum.CurriculumID;
                lang = publication?.CurriculumLanguage.LanguageCode;
            }
            else if (CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext) != Guid.Empty)
            {
                curriculumID = CodeHelper.GetCurriculumID(httpContextAccessor.HttpContext);
            }

            return curriculumID;
        }

        #endregion
    }
}