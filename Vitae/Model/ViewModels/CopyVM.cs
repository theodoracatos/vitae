using Library.Resources;

using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class CopyVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.Copy), Prompt = nameof(SharedResource.Copy))]
        public bool Copy { get; set; }

        public bool Show { get; set; }
    }
}
