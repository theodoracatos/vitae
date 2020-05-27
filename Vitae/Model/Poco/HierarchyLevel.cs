using System.ComponentModel.DataAnnotations;

namespace Model.Poco
{
    public class HierarchyLevel
    {
        [Key]
        public long HierarchyLevelID { get; set; }

        public int HierarchyLevelCode { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Name_de { get; set; }

        [MaxLength(100)]
        public string Name_fr { get; set; }

        [MaxLength(100)]
        public string Name_it { get; set; }

        [MaxLength(100)]
        public string Name_es { get; set; }
    }
}