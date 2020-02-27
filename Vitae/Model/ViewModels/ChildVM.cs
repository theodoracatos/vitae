using Library.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.ViewModels
{
    public class ChildVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Firstname), Prompt = nameof(SharedResource.Firstname))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Firstname { get; set; }

        public int Birthday_Day { get; set; }

        public int Birthday_Month { get; set; }
        public int Birthday_Year { get; set; }

        public int Order { get; set; }
    }
}
