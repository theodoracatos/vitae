using Model.Enumerations;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class SocialLink
    {
        [Key]
        public Guid SocialLinkID { get; set; }

        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(255)]
        public string Link { get; set; }

        public int Order { get; set; }
    }
}