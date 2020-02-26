using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class LanguageSkill
    {
        [Key]
        public Guid LanguageSkillID { get; set; }

        public float Rate { get; set; }

        public int Order { get; set; }

        [Required]
        public virtual Language Language { get; set; }
    }
}