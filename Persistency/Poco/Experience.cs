using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Experience
    {
        [Key]
        public int ExperienceID { get; set; }

        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(1000)]
        public string CompanyLink { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(1000)]
        public string Resumee { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}
