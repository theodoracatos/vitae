using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Award : Base
    {
        [Key]
        public long AwardID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string AwardedFrom { get; set; }

        [MaxLength(255)]
        public string Link { get; set; }

        public DateTime AwardedOn { get; set; }
    }
}