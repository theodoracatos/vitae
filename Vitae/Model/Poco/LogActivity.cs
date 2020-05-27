using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class LogActivity : LogBase
    {
        [Key]
        public long LogActivityID { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
