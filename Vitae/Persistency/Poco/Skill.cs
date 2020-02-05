using System;
using System.ComponentModel.DataAnnotations;

namespace Persistency.Poco
{
    public class Skill
    {
        [Key]
        public Guid SkillID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public float? Rate { get; set; }
    }
}