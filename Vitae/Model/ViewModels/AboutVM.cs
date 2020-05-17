using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class AboutVM : BaseVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.AcademicTitle), Prompt = nameof(SharedResource.AcademicTitle))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string AcademicTitle { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Slogan), Prompt = nameof(SharedResource.Slogan))]
        [MaxLength(4000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Slogan { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Photo), Prompt = nameof(SharedResource.Photo))]
        public string Photo { get; set; }

        public VfileVM Vfile { get; set; }
    }
}