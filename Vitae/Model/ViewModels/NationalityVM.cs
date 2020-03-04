using Library.Resources;

using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class NationalityVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Nationality), Prompt = nameof(SharedResource.Nationality))]
        public string CountryCode { get; set; }

        public int Order { get; set; }
    }
}