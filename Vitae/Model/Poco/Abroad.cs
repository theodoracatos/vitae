using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Abroad : Base
    {
        [Key]
        public Guid AbroadID { get; set; }

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