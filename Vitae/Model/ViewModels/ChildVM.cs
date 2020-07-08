using Library.Resources;

using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class ChildVM : BaseVM
    {
        [Required(ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.Required))]
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Firstname), Prompt = nameof(SharedResource.Firstname))]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Firstname { get; set; }

        public int Birthday_Day { get; set; }
        public int Birthday_Month { get; set; }
        public int Birthday_Year { get; set; }

        [BindNever]
        public DateTime Birthday_Date
        {
            get
            {
                return new DateTime(Birthday_Year, Birthday_Month, Birthday_Day);
            }
        }
    }
}