using Library.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class AboutVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Slogan), Prompt = nameof(SharedResource.Slogan))]
        [MaxLength(4000)]
        [BindProperty]
        public string Slogan { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Photo), Prompt = nameof(SharedResource.Photo))]
        [MaxLength(100)]
        [BindProperty]
        public string Photo { get; set; }

        public IFormFile CV { get; set; }
    }
}