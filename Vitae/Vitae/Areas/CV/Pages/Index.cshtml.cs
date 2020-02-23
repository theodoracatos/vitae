using Library.Repository;
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

        private readonly VitaeContext vitaeContext;
        private readonly Repository repository;

        #endregion

        public PersonVM Person { get; set; } = new PersonVM();
        public AboutVM About { get; set; } = new AboutVM();

        public IList<SocialLinkVM> SocialLinks { get; set; } = new List<SocialLinkVM>();
        public IList<ExperienceVM> Experiences { get; set; } = new List<ExperienceVM>();
        public IList<EducationVM> Educations { get; set; } = new List<EducationVM>();
        public IList<LanguageSkillVM> LanguageSkills { get; set; } = new List<LanguageSkillVM>();

        public Guid Guid { get; set; }
        public string QRTag { get; set; }

        public IndexModel(VitaeContext vitaeContext, Repository repository)
        {
            this.vitaeContext = vitaeContext;
            this.repository = repository;
        }

        public IActionResult OnGet(Guid id)
        {
            if(id == Guid.Empty || !vitaeContext.Curriculums.Any(c => c.Identifier == id))
            {
                return NotFound();
            }
            else if (vitaeContext.Curriculums.Include(c => c.Person).Single(c => c.Identifier == id).Person == null)
            {
                return BadRequest();
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
            Person = repository.GetPersonVM(id);

            //// Social links
            //SocialLinks = new List<SocialLinkVM>();
            //foreach (var socialLink in curriculum.Person.SocialLinks.OrderByDescending(s => s.Order))
            //{
            //    SocialLinks.Add(new SocialLinkVM()
            //    { 
            //        SocialPlatform = socialLink.SocialPlatform,
            //        Link = socialLink.Link
            //    });
            //}

            //// Experiences
            //Experiences = new List<ExperienceVM>();
            //foreach (var experience in curriculum.Person.Experiences.OrderByDescending(e => e.Start))
            //{
            //    Experiences.Add(new ExperienceVM()
            //    {
            //        JobTitle = experience.JobTitle,
            //        CompanyName = experience.CompanyName,
            //        Link = experience.Link,
            //        City = experience.City,
            //        Resumee = experience.Resumee,
            //        //Start = experience.Start,
            //        //End = experience.End
            //    });
            //}

            //// Education
            //Educations = new List<EducationVM>();
            //foreach (var education in curriculum.Person.Educations.OrderByDescending(e => e.Start))
            //{
            //    Educations.Add(new EducationVM()
            //    {
            //        SchoolName = education.SchoolName,
            //        Link = education.Link,
            //        Subject = education.Subject,
            //        City = education.City,
            //        Title = education.Title,
            //        Resumee = education.Resumee,
            //        Start_Month = education.Start.Month,
            //        Start_Year = education.Start.Year,
            //        End_Month = education.End.Value.Month,
            //        End_Year = education.End.Value.Year,
            //        Grade = education.Grade
            //    });
            //}

            //// Languages
            //LanguageSkills = new List<LanguageSkillVM>();
            //foreach (var languageSkill in curriculum.Person.LanguageSkills)
            //{
            //    LanguageSkills.Add(new LanguageSkillVM()
            //    {
            //        //Language = new LanguageVM()
            //        //{
            //        //    Name = languageSkill.Language.Name,
            //        //    LanguageCode = languageSkill.Language.LanguageCode
            //        //},
            //         Rate = languageSkill.Rate
            //    });
            //}
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