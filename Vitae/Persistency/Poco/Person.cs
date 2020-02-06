using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Gender { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string StreetNo { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public string MobileNumber { get; set; }

        public virtual Country Country { get; set; }
        public virtual About About { get; set; }
        public virtual Language Language { get; set; }

        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<LanguageSkill> LanguageSkills { get; set; }
        public virtual ICollection<SocialLink> SocialLinks { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}