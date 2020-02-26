using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Education
    {
        [Key]
        public Guid EducationID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

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

        public int Order { get; set; }

        public virtual Country Country { get; set; }
    }
}