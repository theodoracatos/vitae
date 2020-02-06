﻿using Library.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class LanguageSkillVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Rate), Prompt = nameof(SharedResource.Rate))]
        [Range(1, 4, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public float Rate { get; set; }

        public int Order { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        public string LanguageCode { get; set; }
    }
}