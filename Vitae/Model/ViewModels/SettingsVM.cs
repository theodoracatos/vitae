using Library.Resources;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class SettingsVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ShortIdentifier), Prompt = nameof(SharedResource.ShortIdentifier))]
        [MaxLength(200, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string ShortIdentifier { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LongIdentifier), Prompt = nameof(SharedResource.LongIdentifier))]
        [MaxLength(36, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string LongIdentifier { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Password), Prompt = nameof(SharedResource.Password))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Password { get; set; }

        public bool AnonymizeVitae { get; set; }
        public bool EnableShortIdentifier { get; set; }
        public bool EnablePassword { get; set; }
    }
}
