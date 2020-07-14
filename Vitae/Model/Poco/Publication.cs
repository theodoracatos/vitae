using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Publication : Base
    {
        [Key]
        public long PublicationID { get; set; }

        public Guid PublicationIdentifier { get; set; }

        public bool Anonymize { get; set; }

        public bool Secure { get; set; }

        [MaxLength(250)]
        public string Password { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [MaxLength(100)]
        [Required]
        public string Color { get; set; }

        public bool EnableCVDownload { get; set; }

        public bool EnableDocumentsDownload { get; set; }

        public virtual Curriculum Curriculum { get; set; }
    }
}