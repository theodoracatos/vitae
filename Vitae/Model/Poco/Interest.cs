using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Interest
    {
        [Key]
        public Guid InterestID { get; set; }

        [Required]
        [MaxLength(100)]
        public string InterestName { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        public int Order { get; set; }
    }
}