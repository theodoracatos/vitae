using System.ComponentModel.DataAnnotations;

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
        public string Nicename { get; set; }

        [MinLength(3)]
        [MaxLength(3)]
        public string Iso3 { get; set; }

        public int Numcode { get; set; }

        public int Phonecode { get; set; }
    }
}