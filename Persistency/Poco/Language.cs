using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Language
    {
        [Key]
        public int LanguageID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(2)]
        public string IsoCode { get; set; }
    }
}
