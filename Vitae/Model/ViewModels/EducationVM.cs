﻿using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class EducationVM : BaseVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SchoolName), Prompt = nameof(SharedResource.SchoolName))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string SchoolName { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.URL), Prompt = nameof(SharedResource.URL))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryName), Prompt = nameof(SharedResource.CountryName))]
        public string CountryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.City), Prompt = nameof(SharedResource.City))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Title), Prompt = nameof(SharedResource.Title))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Subject), Prompt = nameof(SharedResource.Subject))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Subject { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Resumee), Prompt = nameof(SharedResource.Resumee))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Grade), Prompt = nameof(SharedResource.Grade))]
        [Range(0, float.MaxValue, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [DisplayFormat(DataFormatString = "{0:$#.##}")]
        public float? Grade { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Start), Prompt = nameof(SharedResource.Start))]
        public int Start_Month { get; set; }

        public int Start_Year { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.End), Prompt = nameof(SharedResource.End))]
        public int? End_Month { get; set; }

        public int? End_Year { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.UntilNow), Prompt = nameof(SharedResource.UntilNow))]
        public bool UntilNow { get; set; }
    }
}