using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class Skill
    {
        [Key]
        public Guid SkillID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(1000)]
        public string Skillset { get; set; }

        public int Order { get; set; }
    }
}