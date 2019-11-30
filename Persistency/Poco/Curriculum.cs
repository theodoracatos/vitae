﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistency.Poco
{
    public class Curriculum
    {
        [Key]
        public int CurriculumID { get; set; }
        public Guid Identifier { get; set; }
        public string FriendlyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Person Person { get; set; }
    }
}
