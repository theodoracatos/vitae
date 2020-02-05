using System;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Vfile
    {
        [Key]
        public Guid VfileID { get; set; }

        public Guid Identifier { get; set; }

        public byte[] Content { get; set; }

        public string MimeType { get; set; }

        [MaxLength(1000)]
        public string FileName { get; set; }
    }
}