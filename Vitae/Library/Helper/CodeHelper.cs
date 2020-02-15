using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Library.Helper
{
    public static class CodeHelper
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
