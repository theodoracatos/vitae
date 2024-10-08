﻿using Library.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Model.Attriutes;
using Model.Helper;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class ExperienceVM : BaseVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.JobTitle), Prompt = nameof(SharedResource.JobTitle))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string JobTitle { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CompanyName), Prompt = nameof(SharedResource.CompanyName))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string CompanyName { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CompanyDescription), Prompt = nameof(SharedResource.CompanyDescription))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string CompanyDescription { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Industry), Prompt = nameof(SharedResource.Industry))]
        public int IndustryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.HierarchyLevel), Prompt = nameof(SharedResource.HierarchyLevel))]
        public int HierarchyLevelCode { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LinkExperience), Prompt = nameof(SharedResource.LinkExperience))]
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

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Resumee), Prompt = nameof(SharedResource.Resumee))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.HierarchyLevel), Prompt = nameof(SharedResource.HierarchyLevels))]
        public IList<HierarchyLevelVM> HierarchyLevels { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Industry), Prompt = nameof(SharedResource.Industries))]
        public IList<IndustryVM> Industries { get; set; }

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