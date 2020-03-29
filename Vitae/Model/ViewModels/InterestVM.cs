using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class InterestVM : BaseVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Interest), Prompt = nameof(SharedResource.Interest))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string InterestName { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Description), Prompt = nameof(SharedResource.Description))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.URL), Prompt = nameof(SharedResource.URL))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }
    }
}