﻿using Library.Resources;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Model.Attriutes;
using Model.Helper;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class CertificateVM : BaseVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Name), Prompt = nameof(SharedResource.Name))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Name { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Description), Prompt = nameof(SharedResource.Description))]
        [MaxLength(1000, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Description { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Issuer), Prompt = nameof(SharedResource.Issuer))]
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Issuer { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LinkCertificate), Prompt = nameof(SharedResource.LinkCertificate))]
        [Url(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        [MaxLength(255, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Link { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.IssuedOn), Prompt = nameof(SharedResource.IssuedOn))]
        public int Start_Month { get; set; }

        public int Start_Year { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ExpiresOn), Prompt = nameof(SharedResource.ExpiresOn))]
        public int? End_Month { get; set; }

        [DateGreaterThan(nameof(NeverExpires), nameof(Start_Year), nameof(Start_Month), nameof(End_Month))]
        public int? End_Year { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.NeverExpires), Prompt = nameof(SharedResource.NeverExpires))]
        public bool NeverExpires { get; set; }

        [BindNever]
        public DateTime Start_Date
        {
            get
            {
                return new DateTime(Start_Year, Start_Month, 1);
            }
        }

        [BindNever]
        public DateTime? End_Date_Opt
        {
            get
            {
                return !NeverExpires ? new DateTime(End_Year.Value, End_Month.Value, 1) : (DateTime?)null;
            }
        }

        [BindNever]
        public DateDifference Difference_Date
        {
            get
            {
                return !NeverExpires ? new DateDifference(Start_Date, End_Date_Opt.Value) : new DateDifference(Start_Date, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));
            }
        }
    }
}