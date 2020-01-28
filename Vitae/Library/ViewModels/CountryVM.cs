using Library.Resources;

using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class CountryVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryName), Prompt = nameof(SharedResource.CountryName))]
        [MaxLength(100)]
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string CountryCode { get; set; }

        [BindProperty]
        public int? PhoneCode { get; set; }
    }
}