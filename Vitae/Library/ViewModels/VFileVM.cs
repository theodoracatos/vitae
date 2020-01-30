using Library.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.ViewModels
{
    public class VfileVM
    {
        [Display(ResourceType = typeof(SharedResource), Name = nameof(SharedResource.CV), Prompt = nameof(SharedResource.CV))]
        public IFormFile Content { get; set; }

        [BindProperty]
        public Guid Identifier { get; set; }

        [BindProperty]
        public string FileName { get; set; }
    }
}
