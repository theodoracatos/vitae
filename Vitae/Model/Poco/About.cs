using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class About : Base
    {
        [Key]
        public long AboutID { get; set; }

        [MaxLength(100)]
        public string AcademicTitle { get; set; }

        [MaxLength(1000)]
        public string Slogan { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string Photo { get; set; }

        public virtual Vfile Vfile { get; set; }
    }
}