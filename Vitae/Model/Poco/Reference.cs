using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Reference
    {
        public Guid ReferenceID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        public bool Gender { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public string PhoneNumber { get; set; }

        public virtual Country Country { get; set; }

        public bool Hide { get; set; }

        public int Order { get; set; }
    }
}
