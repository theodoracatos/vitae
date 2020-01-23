using Library.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.ViewModels
{
    public class CountryVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.FirstnameRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Name), Prompt = nameof(SharedResource.Name))]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.LastnameRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryCode), Prompt = nameof(SharedResource.CountryCode))]
        [MinLength(2)]
        [MaxLength(2)]
        public string CountryCode { get; set; }

    }
}
