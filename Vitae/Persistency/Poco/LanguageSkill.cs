using System;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class LanguageSkill
    {
        [Key]
        public Guid LanguageSkillID { get; set; }

        public float Rate { get; set; }

        [Required]
        public virtual Language Language { get; set; }

        public int Order { get; set; }
    }
}