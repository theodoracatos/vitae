using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class Share
    {
        [Key]
        public Guid ShareID { get; set; }

        public Guid Identifier { get; set; }

        public bool Anonymize { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string QrCode { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public virtual Curriculum Curriculum { get; set; }
    }
}