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
        Xing,
        [Description("fab fa-instagram")]
        Instagram,
        [Description("fab fa-stack-overflow")]
        StackOverflow,
        [Description("fab fa-youtube")]
        Youtube,
        [Description("fab fa-tumblr-square")]
        Tumblr
    }
}