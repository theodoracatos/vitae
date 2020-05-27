using Model.Enumerations;

using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class SocialLink : Base
    {
        [Key]
        public long SocialLinkID { get; set; }

        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(255)]
        public string Link { get; set; }
    }
}