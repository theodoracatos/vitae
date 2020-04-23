using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class Share
    {
        [Key]
        public Guid ShareID { get; set; }

        public bool Anonymize { get; set; }

        [MaxLength(50)]
        [MinLength(4)]
        public string Password { get; set; }

        public virtual Language CurriculumLanguage { get; set; }

        public virtual Curriculum Curriculum { get; set; }
    }
}