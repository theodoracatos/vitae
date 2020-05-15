using System;

namespace Model.ViewModels
{
    public class CheckVM
    {
        public Guid? PublicationID { get; set; }
        public Guid? CurriculumID { get; set; }
        public string Secret { get; set; }
        public string LanguageCode { get; set; }
        public bool Anonymize { get; set; }
        public bool IsPreview { get; set; }
        public bool Challenge { get; set; }
        public bool IsLoggedIn { get; set; }

        public bool MustCheckPassword { get { return !string.IsNullOrEmpty(Secret); } }
        public bool HasValidCurriculumID { get { return CurriculumID.HasValue && CurriculumID != Guid.Empty; } }
        public bool HasPublicationID { get { return PublicationID.HasValue && PublicationID != Guid.Empty; } }
    }
}