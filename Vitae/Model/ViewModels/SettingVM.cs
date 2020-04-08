using Library.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    public class SettingVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.LanguageVersions), Prompt = nameof(SharedResource.LanguageVersions))]
        public IList<LanguageVM> CurriculumLanguages { get; set; }

        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CopyValues), Prompt = nameof(SharedResource.CopyValues))]
        public IList<CopyVM> Copies { get; set; }
    }
}
