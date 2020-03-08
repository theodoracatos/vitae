using Microsoft.Extensions.Logging;
using Model.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Log
    {
        public Guid LogID { get; set; }

        public LogLevel LogLevel { get; set; }

        [MaxLength(100)]
        public LogArea Area { get; set; }

        [MaxLength(255)]
        public string Page { get; set; }

        [MaxLength(50)]
        public string IpAddress { get; set; }

        [MaxLength(50)]
        public string UserAgent { get; set; }

        [MaxLength(2)]
        public string UserLanguage { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }

        public DateTime Timestamp { get; set; }

        public Curriculum Curriculum { get; set; }
    }
}