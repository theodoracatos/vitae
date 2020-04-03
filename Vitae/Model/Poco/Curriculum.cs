using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Curriculum
    {
        [Key]
        public Guid CurriculumID { get; set; }

        public Guid UserID { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Language { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<CurriculumLanguage> CurriculumLanguages { get; set; }
    }
}