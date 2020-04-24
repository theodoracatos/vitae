using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Publication : Base
    {
        [Key]
        public Guid PublicationID { get; set; }

        public bool Anonymize { get; set; }

        [MaxLength(50)]
        [MinLength(4)]
        public string Password { get; set; }

        public virtual Curriculum Curriculum { get; set; }
    }
}