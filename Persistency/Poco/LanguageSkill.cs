using Library.Enumeration;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class LanguageSkill
    {
        [Key]
        public int LanguageSkillID { get; set; }
        public float Rate { get; set; }
        [Required]
        public virtual Language Language { get; set; }
    }
}
