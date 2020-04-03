using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Certificate : Base
    {
        [Key]
        public Guid CertificateID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Issuer { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime? ExpiresOn { get; set; }
    }
}