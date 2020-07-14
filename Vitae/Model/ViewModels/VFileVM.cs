using Library.Resources;

using Microsoft.AspNetCore.Http;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels
{
    [Serializable]
    public class VfileVM : BaseVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.FurtherDocuments), Prompt = nameof(SharedResource.FurtherDocuments))]
        public IFormFile Content { get; set; }

        public Guid Identifier { get; set; }

        public string FileName { get; set; }
    }
}