using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }
        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(10)]
        public string StreetNo { get; set; }
        [MaxLength(10)]
        public int ZipCode { get; set; }
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public virtual About About { get; set; }
        public virtual ICollection<LanguageSkill> Languages { get; set; }
        public virtual ICollection<SocialLink> SocialLinks { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}
