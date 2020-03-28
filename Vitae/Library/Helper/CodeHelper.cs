using Library.Constants;

using Microsoft.AspNetCore.Http;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Library.Helper
{
    public static class CodeHelper
    {
        private const string MAIL_TEMPLATE = "Mail.html";

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

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static Guid GetCurriculumID(HttpContext httpContext)
        {
            Guid curriculumID = new Guid();
            var identity = httpContext.User.Identities.Single();
            if(identity.Claims.Count() > 0)
            {
                Guid.TryParse(identity.Claims.Single(c => c.Type == Claims.CURRICULUM_ID).Value, out curriculumID);
            }

            return curriculumID;
        }

        public static string GetCalledUri(HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}{httpContext.Request.QueryString}";
        }

        public static string GetUserAgent(HttpContext httpContext)
        {
            return httpContext.Request.Headers.ContainsKey("User-Agent") ? httpContext.Request.Headers["User-Agent"].First() : string.Empty;
        }

        public async static Task<string> GetMailBodyTextAsync(string title, string body, string mailtemplate = MAIL_TEMPLATE)
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

        public static bool IsPdf(Stream stream)
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            while (remaining > 0)
            {
                var amtRead = stream.Read(buf, pos, remaining);
                if (amtRead == 0) return false;
                remaining -= amtRead;
                pos += amtRead;
            }
            stream.Position = 0;
            return pdfBytes.SequenceEqual(buf);
        }

        public static int GetAge(DateTime birthday)
        {
            var thisYearsBirthday = new DateTime(DateTime.Now.Year, birthday.Month, birthday.Day);
            var age = DateTime.Now.Year - birthday.Year;

            if(thisYearsBirthday.Date > DateTime.Now.Date)
            {
                --age;
            }

            return Math.Max(0, age);
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static object GetStaticPropValue(Type type, string propName)
        {
            object propertyValue = null;

            foreach (var property in type.GetProperties().Cast<PropertyInfo>().Where(p => p.Name == propName))
            {
                propertyValue = property.GetValue(null, null);
                break;
            }

            return propertyValue;
        }
    }
}