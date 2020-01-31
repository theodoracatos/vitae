using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class MonthVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public int MonthCode { get; set; }
    }
}