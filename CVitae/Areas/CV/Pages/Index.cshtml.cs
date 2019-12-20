using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Extensions;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Persistency.Data;
using QRCoder;

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
            foreach (var experience in curriculum.Person.Experiences)
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