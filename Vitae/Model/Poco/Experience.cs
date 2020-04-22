using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Experience : Base
    {
        [Key]
        public Guid ExperienceID { get; set; }

        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        [Required]
        public virtual HierarchyLevel HierarchyLevel { get; set; }

        [Required]
        public virtual Industry Industry { get; set; }

        [Required]
        public virtual Country Country { get; set; }

        [Required]
        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }
    }
}