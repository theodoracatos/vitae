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
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public string Street { get; set; }
        public string StreetNo { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        [Required]
        public string Email { get; set; }
        public virtual About About { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<SocialLink> SocialLinks { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}
