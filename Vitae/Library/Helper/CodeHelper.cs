﻿using Library.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

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

        public async static Task<string> GetMailBodyTextAsync(string title, string body, string mailtemplate = "Mail.html")
        {
            StringBuilder bodyText;

            using(var reader = new StreamReader(@$"{AssemblyDirectory}/MailTemplates/{mailtemplate}"))
            {
                bodyText = new StringBuilder(await reader.ReadToEndAsync());
            }
            bodyText.Replace("${TITLE}", title);
            bodyText.Replace("${BODY_TEXT}", body);
            bodyText.Replace("${YEAR}", DateTime.Now.Year.ToString());

            return bodyText.ToString();
        }
    }
}