using System;
using System.Threading;

namespace Library.Extensions
{
    public static class DateExtensions
    {
        private const string DATE_FORMAT_SHORT = "MMMM yyyy";

        private const string DATE_FORMAT_LONG = "dd. MMMM yyyy";

        public static string ToShortDateCultureString(this DateTime date)
        {
            return date.ToString(DATE_FORMAT_SHORT, Thread.CurrentThread.CurrentUICulture);
        }

        public static string ToLongDateCultureString(this DateTime date)
        {
            return date.ToString(DATE_FORMAT_LONG, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
