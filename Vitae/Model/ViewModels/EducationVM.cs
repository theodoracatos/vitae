﻿using Library.Resources;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using Model.Attriutes;
using Model.Helper;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class EducationVM : BaseVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.SchoolName), Prompt = nameof(SharedResource.SchoolName))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string SchoolName { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LinkEducation), Prompt = nameof(SharedResource.LinkEducation))]
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

        [DateGreaterThan(nameof(UntilNow), nameof(Start_Year), nameof(Start_Month), nameof(End_Month))]
        public int? End_Year { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.UntilNow), Prompt = nameof(SharedResource.UntilNow))]
        public bool UntilNow { get; set; }

        [BindNever]
        public DateTime Start_Date
        {
            get
            {
                return new DateTime(Start_Year, Start_Month, 1);
            }
        }

        [BindNever]
        public DateTime? End_Date
        {
            get
            {
                return !UntilNow ? new DateTime(End_Year.Value, End_Month.Value, 1) : (DateTime?)null;
            }
        }

        [BindNever]
        public DateDifference Difference_Date
        {
            get
            {
                return !UntilNow ? new DateDifference(Start_Date, End_Date.Value) : new DateDifference(Start_Date, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            }
        }
    }
}