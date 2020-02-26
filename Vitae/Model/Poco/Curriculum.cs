﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Curriculum
    {
        [Key]
        public Guid CurriculumID { get; set; }

        public Guid UserID { get; set; }

        public Guid Identifier { get; set; }

        public string ShortIdentifier { get; set; }

        [MaxLength(100)]
        public string FriendlyId { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual Person Person { get; set; }
    }
}