using Model.Enumerations;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class SocialLink : Base
    {
        [Key]
        public Guid SocialLinkID { get; set; }

        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(255)]
        public string Link { get; set; }
    }
}