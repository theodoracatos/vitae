using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Award
    {
        [Key]
        public int AwardID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string AwardedFrom { get; set; }
        public DateTime AwardedOn { get; set; }
    }
}
