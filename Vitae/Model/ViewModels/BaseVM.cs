using System;
using System.Collections.Generic;

namespace Model.ViewModels
{
    [Serializable]
    public abstract class BaseVM
    {
        public bool Collapsed { get; set; }
        public int Order { get; set; }

        public string CurriculumLanguageCode { get; set; }
        public IEnumerable<LanguageVM> CurriculumLanguages { get; set; }
    }
}