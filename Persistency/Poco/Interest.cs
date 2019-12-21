using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Interest
    {
        [Key]
        public int InterestID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}