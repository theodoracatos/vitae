using System;

namespace Model.Poco
{
    public class PersonCountry
    {
        public Guid PersonID { get; set; }
        public Person Person { get; set; }

        public Guid CountryID { get; set; }
        public Country Country { get; set; }

        public int Order { get; set; }
    }
}