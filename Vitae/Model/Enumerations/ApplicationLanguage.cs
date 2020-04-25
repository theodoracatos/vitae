using Library.Attributes;
using Library.Resources;

namespace Model.Enumerations
{
    public enum ApplicationLanguage
    {
        [LocalizedDescription(nameof(SharedResource.English), typeof(SharedResource))]
        en,
        [LocalizedDescription(nameof(SharedResource.German), typeof(SharedResource))]
        de,
        [LocalizedDescription(nameof(SharedResource.French), typeof(SharedResource))]
        fr,
        [LocalizedDescription(nameof(SharedResource.Italian), typeof(SharedResource))]
        it,
        [LocalizedDescription(nameof(SharedResource.Spanish), typeof(SharedResource))]
        es,
    }
}