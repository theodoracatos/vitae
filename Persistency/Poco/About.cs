using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistency.Poco
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }

        [Required]
        [Column(TypeName = "varchar(MAX)")]
        public string Photo { get; set; }

        [MaxLength(4000)]
        public string Slogan { get; set; }

        public byte[] CV { get; set; }
    }
}