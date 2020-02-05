﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Award
    {
        [Key]
        public Guid AwardID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string AwardedFrom { get; set; }

        public DateTime AwardedOn { get; set; }
    }
}