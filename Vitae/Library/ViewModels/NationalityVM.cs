using Library.Resources;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class NationalityVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Nationality), Prompt = nameof(SharedResource.Nationality))]
        [BindProperty]
        public string CountryCode { get; set; }
    }
}