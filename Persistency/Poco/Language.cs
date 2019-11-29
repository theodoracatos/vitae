using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        [Required]
        public string Name { get; set; }
        public float Rate { get; set; }
    }
}
