using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Interest : Base
    {
        [Key]
        public long InterestID { get; set; }

        [Required]
        [MaxLength(100)]
        public string InterestName { get; set; }

        [MaxLength(100)]
        public string Association { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }
    }
}