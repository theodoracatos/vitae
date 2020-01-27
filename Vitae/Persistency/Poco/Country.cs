﻿using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Country
    { 
        [Key]
        public int CountryID { get; set; }

        [MinLength(2)]
        [MaxLength(2)]
        public string CountryCode { get; set; }


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

        [MinLength(3)]
        [MaxLength(3)]
        public string Iso3 { get; set; }

        public int? NumCode { get; set; }

        public int PhoneCode { get; set; }
    }
}