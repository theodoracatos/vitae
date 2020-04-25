﻿using Library.Constants;

using Microsoft.AspNetCore.Http;
using QRCoder;
using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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

        public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }

        public static TEntity ShallowCopyEntity<TEntity>(TEntity source) where TEntity : class, new()
        {
            // Get properties from EF that are read/write and not marked witht he NotMappedAttribute
            var sourceProperties = typeof(TEntity)
                                    .GetProperties()
                                    .Where(p => p.CanRead && p.CanWrite &&
                                                p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute), true).Length == 0);
            //var notVirtualProperties = sourceProperties.Where(p => !p.GetGetMethod().IsVirtual);
            var newObj = new TEntity();

            foreach (var property in sourceProperties)
            {
                if (Attribute.IsDefined(property, typeof(KeyAttribute)))
                {
                    property.SetValue(newObj, Guid.Empty, null);
                }
                else
                {
                    // Copy value
                    property.SetValue(newObj, property.GetValue(source, null), null);
                }
            }

            return newObj;
        }

        public static string CreateQRCode(string uri)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(uri, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                var bitmap = qrCode.GetGraphic(3);
                var imageBytes = BitmapToBytes(bitmap);
                return Convert.ToBase64String(imageBytes);
            }
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}