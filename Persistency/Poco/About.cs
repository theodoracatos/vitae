using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class About
    {
        [Required]
        public byte[] Photo { get; set; }
        public string Slogan { get; set; }
        public byte[] CV { get; set; }
    }
}
