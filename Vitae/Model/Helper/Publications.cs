using Model.Enumerations;

using System.Collections.Generic;

namespace Model.Helper
{
    public static class Publications
    {
        public static Dictionary<string, string> PublicationSet = new Dictionary<string, string>()
        {
            { ApplicationLanguage.en.ToString(), "f87f0d20-e0ee-48a1-97fc-917b524869f3" },
            { ApplicationLanguage.de.ToString(), "3ddc167c-0cc0-4316-a891-3f91f0d361e7" },
            { ApplicationLanguage.fr.ToString(), "42ee1d28-e605-4d85-ad65-1eb4ee83e840" },
            { ApplicationLanguage.it.ToString(), "80f2fbdf-42c6-45f2-b586-bd7c9afed02f" },
            { ApplicationLanguage.es.ToString(), "9273d56e-29d3-49c8-adbc-5d4fd3e4b2f5" },
        };
    }
}