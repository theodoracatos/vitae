using Library.Resources;

using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Attriutes
{
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private string untilNowProperty, startYearProperty, startMonthProperty, startDayProperty, endMonthProperty, endDayProperty;

        public string GetErrorMessage() => $"{SharedResource.DateCompareError}";

        public DateGreaterThanAttribute(string untilNowProperty, string startYearProperty, string startMonthProperty, string endMonthProperty, string startDayProperty = null, string endDayProperty = null)
        {
            this.untilNowProperty = untilNowProperty;
            this.startYearProperty = startYearProperty;
            this.startMonthProperty = startMonthProperty;
            this.startDayProperty = startDayProperty;
            this.endMonthProperty = endMonthProperty;
            this.endDayProperty = endDayProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var untilNow = GetPropertyValue<bool>(validationContext, untilNowProperty);
                var startDate = new DateTime(GetPropertyValue<int>(validationContext, startYearProperty), GetPropertyValue<int>(validationContext, startMonthProperty), GetPropertyValue(validationContext, startDayProperty, 1));
                var endDate = new DateTime((int)value, GetPropertyValue<int>(validationContext, endMonthProperty), GetPropertyValue(validationContext, endDayProperty, 1));

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