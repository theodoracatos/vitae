using Model.Enumerations;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class PersonalDetail : Base
    {
        [Key]
        public Guid PersonalDetailID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        public bool Gender { get; set; }

        [MaxLength(100)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string StreetNo { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Citizenship { get; set; }

        [Required]
        public MaritalStatus MaritalStatus { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey("SpokenLanguageID")]
        public virtual Language SpokenLanguage { get; set; }

        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<PersonCountry> PersonCountries { get; set; }
    }
}
