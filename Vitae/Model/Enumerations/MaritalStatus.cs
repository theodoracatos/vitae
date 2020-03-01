using Library.Attributes;
using Library.Resources;

namespace Model.Enumerations
{
    public enum MaritalStatus
    {
        [LocalizedDescription(nameof(SharedResource.NoInformation), typeof(SharedResource))]
        NoInformation,
        [LocalizedDescription(nameof(SharedResource.Single), typeof(SharedResource))]
        Single,
        [LocalizedDescription(nameof(SharedResource.Married), typeof(SharedResource))]
        Married,
        [LocalizedDescription(nameof(SharedResource.Concubinage), typeof(SharedResource))]
        Concubinage,
        [LocalizedDescription(nameof(SharedResource.Divorced), typeof(SharedResource))]
        Divorced,
        [LocalizedDescription(nameof(SharedResource.Widowed), typeof(SharedResource))]
        Widowed,
        [LocalizedDescription(nameof(SharedResource.VariousMaritalStatus), typeof(SharedResource))]
        VariousMartialStatus,
    }
}