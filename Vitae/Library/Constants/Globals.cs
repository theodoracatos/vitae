using System.Collections.Generic;

namespace Library.Constants
{
    public static class Globals
    {
        public static readonly List<string> DEMO_PUBLICATIONIDS = new List<string>()
        {
            "f87f0d20-e0ee-48a1-97fc-917b524869f3",
            "3ddc167c-0cc0-4316-a891-3f91f0d361e7",
            "42ee1d28-e605-4d85-ad65-1eb4ee83e840",
            "80f2fbdf-42c6-45f2-b586-bd7c9afed02f",
            "9273d56e-29d3-49c8-adbc-5d4fd3e4b2f5"
        };
        public const int YEAR_START = 1900;

        public const string APPLICATION_URL = "https://myvitae.ch";
        public const string APPLICATION_NAME = "myVitae";
        public const string LOGO = "logo.png";
        public const string VITAE_URL = "https://myvitae.ch";
        public const string GENERAL_INFORMATION = "GeneralInformation";
        public const string GENERAL_INFORMATION_LINK = "/" + GENERAL_INFORMATION + "#subframe";

        public const string INFO_VITAE_MAIL = "info@myvitae.ch";
        public const string ADMIN_VITAE_MAIL = "admin@myvitae.ch";
        public const string ATH_VITAE_MAIL = "alexandros.theodoracatos@myvitae.ch";
        public const string ATH_VITAE_PHONE = "+41 76 611 50 63";
        public const string DEFAULT_LANGUAGE = "de";
        public const string MIME_PDF = "application/pdf";
        public const string REGEX_PASSWORD = "(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{6,}";
        public const string TEST_URL = "/CV/{0}?culture={1}";
        public const string GOOGLE_CAPCHA = "https://www.google.com/recaptcha/api/siteverify";
        public const string GOOGLE_CAPTCHA_API = "https://www.google.com/recaptcha/api.js";
        public const string GOOGLE_ANALYTICS_DEACTIVATOR = "https://tools.google.com/dlpage/gaoptout";
        public const string GOOGLE_ANALYTICS_INFO = "https://support.google.com/analytics/answer/6004245";

        public const string GOOGLE_CHROME_LINK = "https://support.google.com/chrome/answer/95647";
        public const string INTERNET_EXPLORER_LINK = "https://support.microsoft.com/de-de/help/17442/windows-internet-explorer-delete-manage-cookies";
        public const string FIREFOX_LINK = "https://support.mozilla.org/en-US/kb/disable-third-party-cookies";
        public const string SAFARI_LINK = "https://support.apple.com/kb/PH21411";

        public const string DEFAULT_BACKGROUND_COLOR = "rgba(0,0,0,0.9)";
        public const string DEFAULT_FOREGROUND_COLOR = "rgba(255,255,255,255,0.8)";
    }
}