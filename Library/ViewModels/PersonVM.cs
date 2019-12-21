using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class PersonVM
    {
        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string StreetNo { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(10)]
        public int ZipCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Slogan { get; set; }
        public string Photo { get; set; }

        public IList<SocialLinkVM> SocialLinks { get; set; }
        public IList<ExperienceVM> Experiences { get; set; }
        public IList<EducationVM> Educations { get; set; }
    }
}
