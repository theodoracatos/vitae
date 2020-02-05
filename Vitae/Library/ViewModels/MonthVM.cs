using Library.Resources;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class MonthVM
    {
        [Required]
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Name { get; set; }

        [Required]
        [MaxLength(2, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public int MonthCode { get; set; }
    }
}