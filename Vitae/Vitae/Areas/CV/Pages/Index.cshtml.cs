using Library.Repository;
using Library.Resources;
using Library.Helper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Model.Enumerations;
using Model.ViewModels;

using Persistency.Data;

using QRCoder;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

using Vitae.Code;
using System.Linq;

namespace Vitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : BasePageModel
    {
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

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var curriculum = await repository.GetCurriculumByWeakIdentifierAsync(id);

            if(curriculum == null)
            {
                return NotFound();
            }
            else
            {
                PersonalDetail = repository.GetPersonalDetail(curriculum);
                Abouts = repository.GetAbouts(curriculum, "de");
                SocialLinks = repository.GetSocialLinks(curriculum, "de");
                Educations = repository.GetEducations(curriculum, "de");
                Experiences = repository.GetExperiences(curriculum, "de");
                Courses = repository.GetCourses(curriculum, "de");
                Abroads = repository.GetAbroads(curriculum, "de");
                LanguageSkills = repository.GetLanguageSkills(curriculum, "de");
                Interests = repository.GetInterests(curriculum, "de");
                Awards = repository.GetAwards(curriculum, "de");
                Skills = repository.GetSkills(curriculum, "de");
                Certificates = repository.GetCertificates(curriculum, "de");
                References = repository.GetReferences(curriculum, "de");

                FillSelectionViewModel();

                if (base.curriculumID == Guid.Empty)
                {
                    repository.Log(curriculum.CurriculumID, LogArea.Access, LogLevel.Information, CodeHelper.GetCalledUri(httpContext), CodeHelper.GetUserAgent(httpContext), requestCulture.RequestCulture.UICulture.Name, httpContext.Connection.RemoteIpAddress.ToString());
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

        private string CreateQRCode(Guid id)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode($"https//localhost/" + id.ToString(), QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var bitmap = qrCode.GetGraphic(3);
                var imageBytes = BitmapToBytes(bitmap);
                return Convert.ToBase64String(imageBytes);
            }
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        protected override void FillSelectionViewModel() 
        {
            Languages = repository.GetLanguages(requestCulture.RequestCulture.UICulture.Name);
            Countries = repository.GetCountries(requestCulture.RequestCulture.UICulture.Name);
            MaritalStatuses = repository.GetMaritalStatuses(requestCulture.RequestCulture.UICulture.Name);
        }

        #endregion
    }
}
 