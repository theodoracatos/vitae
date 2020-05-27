using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Poco
{
    public class LanguageSkill : Base
    {
        [Key]
        public long LanguageSkillID { get; set; }

        public float Rate { get; set; }

        [ForeignKey("SpokenLanguageID")]
        public virtual Language SpokenLanguage { get; set; }
    }
}