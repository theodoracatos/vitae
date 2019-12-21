using Library.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class SocialLinkVM
    {
        [Required]
        public SocialPlatform SocialPlatform { get; set; }

        [Required]
        [MaxLength(100)]
        public string Hyperlink { get; set; }
    }
}
