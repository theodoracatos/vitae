using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Month
    {
        [Key]
        public int MonthID { get; set; }

        public int MonthCode { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Name_de { get; set; }

        [MaxLength(100)]
        public string Name_fr { get; set; }

        [MaxLength(100)]
        public string Name_it { get; set; }

        [MaxLength(100)]
        public string Name_es { get; set; }
    }
}
