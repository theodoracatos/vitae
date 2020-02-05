using Library.Resources;

using Microsoft.AspNetCore.Http;

using System;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class VfileVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CV), Prompt = nameof(SharedResource.CV))]
        public IFormFile Content { get; set; }

        public Guid Identifier { get; set; }

        public string FileName { get; set; }
    }
}
