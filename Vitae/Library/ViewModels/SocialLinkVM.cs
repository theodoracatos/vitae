﻿using Library.Enumerations;
using Library.Resources;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class SocialLinkVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SocialPlatform), Prompt = nameof(SharedResource.SocialPlatform))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        public SocialPlatform SocialPlatform { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.URL), Prompt = nameof(SharedResource.URL))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }

        public int Order { get; set; }
    }
}