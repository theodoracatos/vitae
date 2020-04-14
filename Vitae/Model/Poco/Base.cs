namespace Model.Poco
{
    public abstract class Base
    {
        public int Order { get; set; }

        public virtual Language CurriculumLanguage { get; set; }
    }
}
