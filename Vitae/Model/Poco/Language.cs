using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Language
    {
        [Key]
        public Guid LanguageID { get; set; }

        [Required]
        [MaxLength(3)]
        public string LanguageCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name_de { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name_fr { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name_it { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name_es { get; set; }
    }
}