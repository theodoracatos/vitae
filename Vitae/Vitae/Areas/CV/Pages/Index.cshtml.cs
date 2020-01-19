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

        public PersonVM PersonVM { get; set; } = new PersonVM();

        public Guid Guid { get; set; }
        public string QRTag { get; set; }

        public IndexModel(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public IActionResult OnGet(Guid id)
        {
            if (appContext.Curriculums.Any(c => c.Identifier == id))
            {
                FillValues(id);

                Guid = id;
                QRTag = CreateQRCode(id);

                return Page();
            }
            else
            {
                return NotFound();
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
            PersonVM.Firstname = curriculum.Person.Firstname;
            PersonVM.Lastname = curriculum.Person.Lastname;
            PersonVM.Street = curriculum.Person.Street;
            PersonVM.StreetNo = curriculum.Person.StreetNo;
            PersonVM.City = curriculum.Person.City;
            PersonVM.ZipCode = curriculum.Person.ZipCode;
            PersonVM.Email = curriculum.Person.Email;
            PersonVM.MobileNumber = curriculum.Person.MobileNumber;
            PersonVM.Slogan = curriculum.Person.About.Slogan;
            PersonVM.Photo = curriculum.Person.About.Photo;

            // Social links
            PersonVM.SocialLinks = new List<SocialLinkVM>();
            foreach (var socialLink in curriculum.Person.SocialLinks.OrderByDescending(s => s.SocialLinkID))
            {
                PersonVM.SocialLinks.Add(new SocialLinkVM()
                { 
                    SocialPlatform = socialLink.SocialPlatform,
                    Hyperlink = socialLink.Hyperlink
                });
            }

            // Experiences
            PersonVM.Experiences = new List<ExperienceVM>();
            foreach (var experience in curriculum.Person.Experiences.OrderByDescending(e => e.Start))
            {
                PersonVM.Experiences.Add(new ExperienceVM()
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
            PersonVM.Educations = new List<EducationVM>();
            foreach (var education in curriculum.Person.Educations.OrderByDescending(e => e.Start))
            {
                PersonVM.Educations.Add(new EducationVM()
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
            PersonVM.LanguageSkills = new List<LanguageSkillVM>();
            foreach (var languageSkill in curriculum.Person.LanguageSkills)
            {
                PersonVM.LanguageSkills.Add(new LanguageSkillVM()
                {
                    Language = new LanguageVM()
                    {
                        Name = languageSkill.Language.Name,
                        IsoCode = languageSkill.Language.IsoCode
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