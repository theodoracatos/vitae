using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string IsoCode { get; set; }
    }
}