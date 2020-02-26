using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Month
    {
        [Key]
        public Guid MonthID { get; set; }

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