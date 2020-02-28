using Library.Attributes;
using Library.Resources;

namespace Model.Enumerations
{
    public enum MartialStatus
    {
        [LocalizedDescription(nameof(SharedResource.NoInformation), typeof(SharedResource))]
        NoInformation,
        [LocalizedDescription(nameof(SharedResource.Single), typeof(SharedResource))]
        Single,
        [LocalizedDescription(nameof(SharedResource.Married), typeof(SharedResource))]
        Married,
        [LocalizedDescription(nameof(SharedResource.Concubinage), typeof(SharedResource))]
        Concubinage,
        [LocalizedDescription(nameof(SharedResource.Awards), typeof(SharedResource))]
        Divorced,
        [LocalizedDescription(nameof(SharedResource.Awards), typeof(SharedResource))]
        Widowed,
    }
}