using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class LanguageVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string IsoCode { get; set; }
    }
}