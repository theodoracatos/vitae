using System;

namespace Model.Poco
{
    public class PersonCountry
    {
        public long PersonalDetailID { get; set; }
        public PersonalDetail PersonalDetail { get; set; }

        public long CountryID { get; set; }
        public Country Country { get; set; }

        public int Order { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;
    }
}