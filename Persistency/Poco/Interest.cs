using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Interest
    {
        [Key]
        public int InterestID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
