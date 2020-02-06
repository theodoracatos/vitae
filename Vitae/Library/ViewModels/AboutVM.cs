using Library.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class AboutVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Slogan), Prompt = nameof(SharedResource.Slogan))]
        [MaxLength(4000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Slogan { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Photo), Prompt = nameof(SharedResource.Photo))]
        public string Photo { get; set; }

        public VfileVM Vfile { get; set; }
    }
}