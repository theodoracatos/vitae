using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Certificate
    {
        [Key]
        public Guid CertificateID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }

        public int Order { get; set; }
    }
}