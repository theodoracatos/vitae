using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistency.Poco
{
    public class Education
    {
        [Key]
        public int EducationID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [MaxLength(100)]
        public string SchoolLink { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [MaxLength(1000)]
        public string Resumee { get; set; }

        public float? Grade { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}
