using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Poco
{
    public abstract class Base
    {
        public int Order { get; set; }

        public virtual Language CurriculumLanguage { get; set; }
    }
}
