using System.Collections.Generic;

namespace Library.ViewModels
{
    public class PersonVM
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Slogan { get; set; }
        public string Photo { get; set; }

        public IList<SocialLinkVM> SocialLinks { get; set; }
        public IList<ExperienceVM> Experiences { get; set; }
    }
}
