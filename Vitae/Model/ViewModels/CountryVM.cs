﻿using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class CountryVM : BaseVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryName), Prompt = nameof(SharedResource.CountryName))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Name { get; set; }

        public string CountryCode { get; set; }

        public int? PhoneCode { get; set; }
    }
}