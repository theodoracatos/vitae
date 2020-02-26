using Microsoft.Extensions.Logging;

using System;

namespace Model.Poco
{
    public class Log
    {
        public Guid LogID { get; set; }

        public LogLevel LogLevel { get; set; }

        public string IpAddress { get; set; }

        public string UserAgent { get; set; }

        public string UserLanguage { get; set; }

        public Guid CurriculumID { get; set; }

        public string Message { get; set; }

        public DateTime Timestamp { get; set; }
    }
}