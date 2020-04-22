using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Attriutes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        public string UntilNow { get; set; }
        public string StartYear { get; set; }
        public string StartMonth { get; set; }
        public string StartDay { get; set; }
        public string EndYear { get; set; }
        public string EndMonth { get; set; }
        public string EndDay { get; set; }

        public string GetErrorMessage() => $"{SharedResource.DateCompareError}";

        public DateGreaterThanAttribute(string untilNowProperty, string startYearProperty, string startMonthProperty, string endMonthProperty, string startDayProperty = null, string endDayProperty = null)
        {
            UntilNow = untilNowProperty;
            StartYear = startYearProperty;
            StartMonth = startMonthProperty;
            StartDay = startDayProperty ?? "";
            EndMonth = endMonthProperty;
            EndDay = endDayProperty ?? "";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var untilNow = GetPropertyValue<bool>(validationContext, UntilNow);
                var startDate = new DateTime(GetPropertyValue<int>(validationContext, StartYear), GetPropertyValue<int>(validationContext, StartMonth), GetPropertyValue(validationContext, StartDay, 1));
                var endDate = new DateTime((int)value, GetPropertyValue<int>(validationContext, EndMonth), GetPropertyValue(validationContext, EndDay, 1));

                if (!untilNow && startDate > endDate)
                {
                    return new ValidationResult(GetErrorMessage());
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            catch
            {
                return ValidationResult.Success;
            }
        }

        private T GetPropertyValue<T>(ValidationContext validationContext, string propertyName, T defaultValue = default(T))
        {
            T propertyValue = defaultValue;

            if(!string.IsNullOrEmpty(propertyName))
            {
                var property = validationContext.ObjectType.GetProperty(propertyName);
                propertyValue = (T)property.GetValue(validationContext.ObjectInstance, null);
            }

            return propertyValue;
        }
    }
}