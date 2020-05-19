using Library.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class PublicationVM : BaseVM
    { 
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.PublicationIdentifier), Prompt = nameof(SharedResource.PublicationIdentifier))]
        [MaxLength(200, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string PublicationIdentifier { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Password), Prompt = nameof(SharedResource.Password))]
        [MaxLength(20, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        public string LanguageCode { get; set; }

        public string QrCode { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.URL), Prompt = nameof(SharedResource.URL))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Anonymize), Prompt = nameof(SharedResource.Anonymize))]
        public bool Anonymize { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Secure), Prompt = nameof(SharedResource.Secure))]
        public bool Secure { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Activate), Prompt = nameof(SharedResource.Activate))]
        public bool EnablePassword { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Notes), Prompt = nameof(SharedResource.Notes))]
        public string Notes { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Color), Prompt = nameof(SharedResource.Color))]
        public string Color { get; set; }
    }
}