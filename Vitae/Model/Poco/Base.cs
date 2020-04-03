using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Poco
{
    public class Base
    {
        public int Order { get; set; }

        public virtual Language Language { get; set; }
    }
}
