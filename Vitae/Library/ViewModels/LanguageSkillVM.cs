using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class LanguageSkillVM
    {
        [Required]
        public float Rate { get; set; }

        [Required]
        public virtual LanguageVM Language { get; set; }
    }
}