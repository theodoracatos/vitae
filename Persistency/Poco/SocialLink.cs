using Library.Enumeration;

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
        public string Hyperlink { get; set; }
    }
}
