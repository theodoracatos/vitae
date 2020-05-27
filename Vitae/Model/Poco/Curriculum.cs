using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Curriculum
    {
        [Key]
        public Guid CurriculumID { get; set; }

        public Guid UserID { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;

        public DateTime LastUpdated { get; set; }

        public Language Language { get; set; }

        public virtual ICollection<PersonalDetail> PersonalDetails { get; set; }
        public virtual ICollection<About> Abouts { get; set; }
        public virtual ICollection<Abroad> Abroads { get; set; }
        public virtual ICollection<Award> Awards { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<LanguageSkill> LanguageSkills { get; set; }
        public virtual ICollection<SocialLink> SocialLinks { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<Reference> References { get; set; }

        public virtual ICollection<CurriculumLanguage> CurriculumLanguages { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
    }
}