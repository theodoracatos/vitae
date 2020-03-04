using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Course
    {
        [Key]
        public Guid CourseID { get; set; }

        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public int Order { get; set; }
    }
}
