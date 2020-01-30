using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Persistency.Poco
{
    public class Vfile
    {
        [Key]
        public int VfileID { get; set; }

        public Guid Identifier { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        [MaxLength(1000)]
        public string FileName { get; set; }
    }
}