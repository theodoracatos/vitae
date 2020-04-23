using Library.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class ShareVM
    { 
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ShortIdentifier), Prompt = nameof(SharedResource.ShareIdentifier))]
        [MaxLength(200, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string ShareIdentifier { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Password), Prompt = nameof(SharedResource.Password))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        public string LanguageCode { get; set; }

        public string QrCode { get; set; }

        public bool Anonymize { get; set; }

        public bool EnablePassword { get; set; }
    }
}