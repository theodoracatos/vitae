using Library.Repository;
using Library.Resources;
using Library.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

using Persistency.Data;

using QRCoder;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Vitae.Code;

namespace CVitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : BasePageModel
    {
        public Guid CurriculumID { get { return curriculumID; } }

        public PersonVM Person { get; set; }
        public AboutVM About { get; set; }

        public IList<AwardVM> Awards { get; set; } = new List<AwardVM>();
        public IList<EducationVM> Educations { get; set; } = new List<EducationVM>();
        public IList<ExperienceVM> Experiences { get; set; } = new List<ExperienceVM>();
        public IList<InterestVM> Interests { get; set; } = new List<InterestVM>();
        public IList<LanguageSkillVM> LanguageSkills { get; set; } = new List<LanguageSkillVM>();
        public IList<SkillVM> Skills { get; set; } = new List<SkillVM>();
        public IList<SocialLinkVM> SocialLinks { get; set; } = new List<SocialLinkVM>();

        public IEnumerable<LanguageVM> Languages { get; set; }

        public IndexModel(IStringLocalizer<SharedResource> localizer, VitaeContext vitaeContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, Repository repository)
    : base(localizer, vitaeContext, httpContextAccessor, userManager, repository) { }

        public IActionResult OnGet(string id)
        {
            var curriculum = repository.GetCurriculumByWeakIdentifier(id);

            if(curriculum == null)
            {
                return NotFound();
            }
            else if(curriculum.Person == null)
            {
                return BadRequest(); // CV not ready yet
            }
            else
            {
                Person = repository.GetPerson(curriculum);
                About = repository.GetAbout(curriculum);
                Awards = repository.GetAwards(curriculum);
                Educations = repository.GetEducations(curriculum);
                Experiences = repository.GetExperiences(curriculum);
                Interests = repository.GetInterests(curriculum);
                LanguageSkills = repository.GetLanguageSkills(curriculum);
                Skills = repository.GetSkills(curriculum);
                SocialLinks = repository.GetSocialLinks(curriculum);

                FillSelectionViewModel();
                return Page();
            }
        }
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
        }
    }
}