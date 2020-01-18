using Library.Enumerations;

using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class SocialLink
    {
        [Key]
        public int SocialLinkID { get; set; }

        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(100)]
        public string Hyperlink { get; set; }
    }
}
