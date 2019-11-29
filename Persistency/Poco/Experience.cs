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
        public string JobTitle { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyLink { get; set; }
        [Required]
        public string City { get; set; }
        public string Resumee { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
