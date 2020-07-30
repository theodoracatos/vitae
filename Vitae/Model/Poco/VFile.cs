using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class Vfile
    {
        [Key]
        public Guid VfileID { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        [MaxLength(100)]
        public string MimeType { get; set; }

        [MaxLength(255)]
        public string FileName { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;

        [ForeignKey("AboutID")]
        public virtual About About { get; set; }
    }
}