using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public abstract class Base
    {
        public int Order { get; set; }

        public virtual Language CurriculumLanguage { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;
    }
}
