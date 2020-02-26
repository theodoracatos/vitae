using Library.Resources;

using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class SkillVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Category), Prompt = nameof(SharedResource.Category))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Category { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Skillset), Prompt = nameof(SharedResource.Etc))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Skillset { get; set; }

        public int Order { get; set; }
    }
}