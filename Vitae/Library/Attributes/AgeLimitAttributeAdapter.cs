using Library.Resources;

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Library.Attributes
{
    public class AgeLimitAttribute : ValidationAttribute
    {
        public AgeLimitAttribute(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        public string GetErrorMessage() =>
            $"{SharedResource.About} ({Limit})";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if ((int)value > DateTime.Now.Year - Limit)
            {
                return new ValidationResult(GetErrorMessage());
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

    public class AgeLimitAttributeAdapter : AttributeAdapterBase<AgeLimitAttribute>
    {
        public AgeLimitAttributeAdapter(AgeLimitAttribute attribute,
            IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-agelimit", GetErrorMessage(context));

            var year = Attribute.Limit.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-agelimit-year", year);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            Attribute.GetErrorMessage();
    }
}