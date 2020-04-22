using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class HierarchyLevelVM : BaseVM
    {
        [MaxLength(100, ErrorMessageResourceType = typeof(SharedResource), ErrorMessageResourceName = nameof(SharedResource.ProperValue))]
        public string Name { get; set; }

        public int HierarchyLevelCode { get; set; }
    }
}