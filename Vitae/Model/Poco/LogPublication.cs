using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class LogPublication : LogBase
    {
        [Key]
        public long LogPublicationID { get; set; }

        public Guid PublicationID { get; set; }
    }
}
