using Library.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class SettingVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LanguageVersions), Prompt = nameof(SharedResource.LanguageVersions))]
        public IList<CurriculumLanguageVM> CurriculumLanguages { get; set; }

        public IList<SettingItemVM> SettingItems { get; set; }
    }
}