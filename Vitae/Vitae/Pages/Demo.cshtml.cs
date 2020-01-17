using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace Vitae.Pages
{
    public enum Country
    {
        Switzerland,
        Germany,
        Austria,
        Italia,
        Greece,
        Spain
    }

    public class DemoModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Name { get; set; }
        [BindProperty]
        [Range(1, 29, ErrorMessage = "Sorry but you are too old for this club")]
        [Required]
        public int Age { get; set; }

        [BindProperty]
        [Display(Name = "Male?")]
        public bool Gender { get; set; }

        [BindProperty]
        [Required]
        public DateTime Birthdate { get; set; }

        [BindProperty]
        [Display(Name = "Person attributes...")]
        [Required]
        public List<string> Attributes { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Country")]
        public Country SelectedCountry { get; set; }

        public void OnGet()
        {
            InitValues();
        }

        public IActionResult OnPostAddField()
        {
            Attributes.Add(string.Empty);

            Thread.Sleep(new Random().Next(200, 1000));

            return GetPartialViewResult("_DemoPartial");
        }

        public IActionResult OnPostRemoveField()
        {
            if (Attributes.Count > 1)
            {
                Attributes.RemoveAt(Attributes.Count - 1);
            }

            Thread.Sleep(new Random().Next(200, 1000));

            return GetPartialViewResult("_DemoPartial");
        }

        public IActionResult OnPostIncrement()
        {
            Age++;

            return GetPartialViewResult("_DemoPartial");
        }

        public IActionResult OnPostDecrement()
        {
            Age--;

            return GetPartialViewResult("_DemoPartial");
        }

        public IActionResult OnPostRedirect()
        {
            return Redirect("/Privacy");
        }

        public IActionResult OnPostReset()
        {
            InitValues();
            ModelState.Clear();

            return Page();
        }

        public IActionResult OnPostSave()
        {
            if (ModelState.IsValid && Name == "Peter")
            {
                // Grant access to club
                return Redirect("/Privacy");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Only Peter has access to the U30 club");
                return Page();
            }
        }

        private void InitValues()
        {
            Name = "Peter";
            Age = 30;
            Birthdate = DateTime.Now.AddYears(-Age);
            Attributes = new List<string>() { "" };
        }


        private PartialViewResult GetPartialViewResult(string viewName)
        {
            // Ajax
            var dataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { { nameof(IndexModel), this } };
            dataDictionary.Model = this;

            PartialViewResult result = new PartialViewResult()
            {
                ViewName = viewName,
                ViewData = dataDictionary,
            };

            return result;
        }
    }
}