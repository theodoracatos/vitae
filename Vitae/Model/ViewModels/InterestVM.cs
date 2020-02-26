using Library.Resources;

using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class InterestVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Name), Prompt = nameof(SharedResource.Name))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Description), Prompt = nameof(SharedResource.Description))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.URL), Prompt = nameof(SharedResource.URL))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }

        public int Order { get; set; }
    }
}