using Library.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Persistency.Data;

using QRCoder;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace CVitae.Areas.CV.Pages
{
    [Area("CV")]
    public class IndexModel : PageModel
    {
        #region Variables

        private readonly ApplicationContext appContext;

        #endregion

        public PersonVM Person { get; set; } = new PersonVM();
        public AboutVM About { get; set; } = new AboutVM();

        public IList<SocialLinkVM> SocialLinks { get; set; }
        public IList<ExperienceVM> Experiences { get; set; }
        public IList<EducationVM> Educations { get; set; }

        public IList<LanguageSkillVM> LanguageSkills { get; set; }

        public Guid Guid { get; set; }
        public string QRTag { get; set; }

        public IndexModel(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public IActionResult OnGet(Guid id)
        {
            if(id == Guid.Empty || !appContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else
            {
                FillValues(id);

                Guid = id;
                QRTag = CreateQRCode(id);

                return Page();
            }
        }

        private void FillValues(Guid id)
        {
            var curriculum = appContext.Curriculums
                    .Include(c => c.Person)
                    .Include(c => c.Person.About)
                    .Include(c => c.Person.SocialLinks)
                    .Include(c => c.Person.Experiences)
                    .Include(c => c.Person.Educations)
                    .Include(c => c.Person.LanguageSkills)
                    .Single(c => c.Identifier == id);

            // Set values
           Person.Firstname = curriculum.Person.Firstname;
           Person.Lastname = curriculum.Person.Lastname;
           Person.Street = curriculum.Person.Street;
           Person.StreetNo = curriculum.Person.StreetNo;
           Person.City = curriculum.Person.City;
           Person.ZipCode = curriculum.Person.ZipCode;
           Person.Email = curriculum.Person.Email;
           Person.MobileNumber = curriculum.Person.MobileNumber;
           About.Slogan = curriculum.Person.About.Slogan;
           About.Photo = curriculum.Person.About.Photo;

            // Social links
            SocialLinks = new List<SocialLinkVM>();
            foreach (var socialLink in curriculum.Person.SocialLinks.OrderByDescending(s => s.SocialLinkID))
            {
                SocialLinks.Add(new SocialLinkVM()
                { 
                    SocialPlatform = socialLink.SocialPlatform,
                    Hyperlink = socialLink.Hyperlink
                });
            }

            // Experiences
            Experiences = new List<ExperienceVM>();
            foreach (var experience in curriculum.Person.Experiences.OrderByDescending(e => e.Start))
            {
                Experiences.Add(new ExperienceVM()
                {
                    JobTitle = experience.JobTitle,
                    CompanyName = experience.CompanyName,
                    CompanyLink = experience.CompanyLink,
                    City = experience.City,
                    Resumee = experience.Resumee,
                    Start = experience.Start,
                    End = experience.End
                });
            }

            // Education
            Educations = new List<EducationVM>();
            foreach (var education in curriculum.Person.Educations.OrderByDescending(e => e.Start))
            {
                Educations.Add(new EducationVM()
                {
                    SchoolName = education.SchoolName,
                    SchoolLink = education.SchoolName,
                    Subject = education.Subject,
                    City = education.City,
                    Title = education.Title,
                    Resumee = education.Resumee,
                    Start = education.Start,
                    End = education.End,
                    Grade = education.Grade
                });
            }

            // Languages
            LanguageSkills = new List<LanguageSkillVM>();
            foreach (var languageSkill in curriculum.Person.LanguageSkills)
            {
                LanguageSkills.Add(new LanguageSkillVM()
                {
                    Language = new LanguageVM()
                    {
                        Name = languageSkill.Language.Name,
                        LanguageCode = languageSkill.Language.LanguageCode
                    },
                     Rate = languageSkill.Rate
                });
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
    }
}