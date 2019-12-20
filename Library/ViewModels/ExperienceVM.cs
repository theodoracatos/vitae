using System;
using System.Collections.Generic;
using System.Text;

namespace Library.ViewModels
{
    public class ExperienceVM
    {   public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLink { get; set; }
        public string City { get; set; }
        public string Resumee { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
