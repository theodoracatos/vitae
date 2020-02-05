using Library.Enumerations;

using System;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class SocialLink
    {
        [Key]
        public Guid SocialLinkID { get; set; }

        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(100)]
        public string Hyperlink { get; set; }

        public int Order { get; set; }
    }
}