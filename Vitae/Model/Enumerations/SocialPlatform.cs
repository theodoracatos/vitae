using System.ComponentModel;

namespace Model.Enumerations
{
    public enum SocialPlatform
    {
        [Description("fab fa-facebook-f")]
        Facebook = 1,
        [Description("fab fa-twitter")]
        Twitter,
        [Description("fab fa-linkedin-in")]
        LinkedIn,
        [Description("fab fa-github")]
        Github,
        [Description("fab fa-xing")]
        Xing
    }
}
