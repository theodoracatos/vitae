using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class About : Base
    {
        [Key]
        public Guid AboutID { get; set; }

        [MaxLength(100)]
        public string AcademicTitle { get; set; }

        [MaxLength(4000)]
        public string Slogan { get; set; }

        [Required]
        [Column(TypeName = "varchar(max)")]
        public string Photo { get; set; }

        public virtual Vfile Vfile { get; set; }
    }
}