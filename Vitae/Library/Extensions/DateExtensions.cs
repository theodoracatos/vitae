using System;
using System.Threading;

namespace Library.Extensions
{
    public static class DateExtensions
    {
        private const string DATE_FORMAT = "MMMM yyyy";

        public static string ToShortDateCultureString(this DateTime date)
        {
            return date.ToString(DATE_FORMAT, Thread.CurrentThread.CurrentUICulture);
        }
    }
}
