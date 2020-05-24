using Microsoft.Extensions.Logging;

using Model.Enumerations;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public abstract class LogBase
    {
        public Guid CurriculumID { get; set; }

        public LogLevel LogLevel { get; set; }

        public LogArea LogArea { get; set; }

        [MaxLength(1000)]
        public string Link { get; set; }

        [MaxLength(50)]
        public string IpAddress { get; set; }

        [MaxLength(1000)]
        public string UserAgent { get; set; }

        [MaxLength(2)]
        public string UserLanguage { get; set; }

        public DateTime Timestamp { get; private set; } = DateTime.Now;
    }
}