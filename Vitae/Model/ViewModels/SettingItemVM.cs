using Library.Resources;

using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class SettingItemVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CopyValues), Prompt = nameof(SharedResource.CopyValues))]
        public bool Copy { get; set; }

        public string FormerLanguageCode { get; set; }

        public int NrOfItems { get; set; }

        public bool HasPublication { get; set; }
    }
}