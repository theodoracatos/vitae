using Library.Resources;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class PersonVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Gender), Prompt = nameof(SharedResource.Gender))]
        public bool? Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Firstname), Prompt = nameof(SharedResource.Firstname))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Lastname), Prompt = nameof(SharedResource.Lastname))]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Birthday), Prompt = nameof(SharedResource.Birthday))]
        public int Birthday_Day { get; set; }

        public int Birthday_Month { get; set; }
        public int Birthday_Year { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Street), Prompt = nameof(SharedResource.Street))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.StreetNo), Prompt = nameof(SharedResource.StreetNo))]
        [MaxLength(10, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string StreetNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.State), Prompt = nameof(SharedResource.State))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.City), Prompt = nameof(SharedResource.City))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ZipCode), Prompt = nameof(SharedResource.ZipCode))]
        [MaxLength(10, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string ZipCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryName), Prompt = nameof(SharedResource.CountryName))]
        public string CountryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.RequiredSelection))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        public string LanguageCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Email), Prompt = nameof(SharedResource.Email))]
        [EmailAddress(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.MobileNumber), Prompt = nameof(SharedResource.MobileNumber))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [RegularExpression(@"^[1-9][0-9 ]{5,15}", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [MaxLength(16, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string MobileNumber { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Nationalities), Prompt = nameof(SharedResource.Nationalities))]
        public IList<NationalityVM> Nationalities { get; set; }
    }
}