﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Child
    {
        [Key]
        public long ChildID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        public DateTime Birthday { get; set; }
    
        public int Order { get; set; }

        public DateTime CreatedOn { get; private set; } = DateTime.Now;
    }
}