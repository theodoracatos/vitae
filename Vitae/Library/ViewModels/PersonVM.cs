using Library.Resources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class PersonVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Gender), Prompt = nameof(SharedResource.Gender))]
        [MaxLength(100)]
        [BindProperty]
        public bool? Gender { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.FirstnameRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Firstname), Prompt = nameof(SharedResource.Firstname))]
        [MaxLength(100)]
        [BindProperty]
        public string Firstname { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.LastnameRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Lastname), Prompt = nameof(SharedResource.Lastname))]
        [MaxLength(100)]
        [BindProperty]
        public string Lastname { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.StreetRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Street), Prompt = nameof(SharedResource.Street))]
        [MaxLength(100)]
        [BindProperty]
        public string Street { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.StreetNoRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.StreetNo), Prompt = nameof(SharedResource.StreetNo))]
        [MaxLength(10)]
        [BindProperty]
        public string StreetNo { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.StateRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.State), Prompt = nameof(SharedResource.State))]
        [MaxLength(100)]
        [BindProperty]
        public string State { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.CityRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.City), Prompt = nameof(SharedResource.City))]
        [MaxLength(100)]
        [BindProperty]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ZipCodeRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.ZipCode), Prompt = nameof(SharedResource.ZipCode))]
        [MaxLength(10)]
        [BindProperty]
        public string ZipCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.CountryRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CountryName), Prompt = nameof(SharedResource.CountryName))]
        [BindProperty]
        public string CountryCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.LanguageRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Language), Prompt = nameof(SharedResource.Language))]
        [BindProperty]
        public string LanguageCode { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.EmailRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Email), Prompt = nameof(SharedResource.Email))]
        [EmailAddress(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.EmailRequired))]
        [MaxLength(100)]
        [BindProperty]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.MobileNumberRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.MobileNumber), Prompt = nameof(SharedResource.MobileNumber))]
        [DataType(DataType.PhoneNumber, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.MobileNumberRequired))]
        [RegularExpression(@"^[1-9][0-9 ]{5,100}", ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.MobileNumberRequired))]
        [MaxLength(100)]
        [BindProperty]
        public string MobileNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.SloganRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Slogan), Prompt = nameof(SharedResource.Slogan))]
        [MaxLength(100)]
        [BindProperty]
        public string Slogan { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.PhotoRequired))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Photo), Prompt = nameof(SharedResource.Photo))]
        [MaxLength(100)]
        [BindProperty]
        public string Photo { get; set; }

        public IList<SocialLinkVM> SocialLinks { get; set; }
        public IList<ExperienceVM> Experiences { get; set; }
        public IList<EducationVM> Educations { get; set; }

        public IList<LanguageSkillVM> LanguageSkills { get; set; }
    }
}