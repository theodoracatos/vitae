using Library.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class EducationVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SchoolName), Prompt = nameof(SharedResource.SchoolName))]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SchoolLink), Prompt = nameof(SharedResource.SchoolLink))]
        [MaxLength(100)]
        public string SchoolLink { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.City), Prompt = nameof(SharedResource.City))]
        [MaxLength(100)]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Title), Prompt = nameof(SharedResource.Title))]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Subject), Prompt = nameof(SharedResource.Subject))]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Resumee), Prompt = nameof(SharedResource.Resumee))]
        [MaxLength(1000)]
        public string Resumee { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Grade), Prompt = nameof(SharedResource.Grade))]
        public float? Grade { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Start), Prompt = nameof(SharedResource.Start))]
        public DateTime Start { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.End), Prompt = nameof(SharedResource.End))]
        public DateTime? End { get; set; }
    }
}