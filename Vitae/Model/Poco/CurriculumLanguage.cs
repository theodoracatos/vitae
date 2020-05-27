using System;

namespace Model.Poco
{
    public class CurriculumLanguage
    {
        public Guid CurriculumID { get; set; }
        public Curriculum Curriculum { get; set; }

        public long LanguageID { get; set; }
        public Language Language { get; set; }

        public int Order { get; set; }
        public bool IsSelected { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;
    }
}