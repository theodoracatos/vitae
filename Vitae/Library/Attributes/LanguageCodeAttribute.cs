using System;

namespace Library.Attributes
{
    public class LanguageCodeAttribute : Attribute
    {
        public string LanguageCode { get; private set; }

        public LanguageCodeAttribute(string languageCode)
        {
            LanguageCode = languageCode;
        }
    }
}
