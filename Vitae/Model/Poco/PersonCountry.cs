using System;

namespace Model.Poco
{
    public class PersonCountry
    {
        public Guid PersonalDetailID { get; set; }
        public PersonalDetail PersonalDetail { get; set; }

        public Guid CountryID { get; set; }
        public Country Country { get; set; }

        public int Order { get; set; }
    }
}