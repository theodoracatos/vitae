using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class PersonCountry
    {
        public int PersonID { get; set; }
        public Person Person { get; set; }

        public int CountryID { get; set; }
        public Country Country { get; set; }

        public int Order { get; set; }
    }
}