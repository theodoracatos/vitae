using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Person
    {
        [Key]
        public Guid PersonID { get; set; }

        public string Language { get; set; }

        public virtual PersonalDetail PersonalDetail { get; set; }
        public virtual About About { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<LanguageSkill> LanguageSkills { get; set; }
        public virtual ICollection<SocialLink> SocialLinks { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
    }
}