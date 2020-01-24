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
    }
}