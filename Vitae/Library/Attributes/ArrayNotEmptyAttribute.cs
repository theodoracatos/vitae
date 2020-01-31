using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Attributes
{
    public class ArrayNotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return new ValidationResult("Not good");

            //return ValidationResult.Success;
        }
    }

    public class ArrayNotEmptyAttributeAdapter : AttributeAdapterBase<ArrayNotEmptyAttribute>
    {
        public ArrayNotEmptyAttributeAdapter(ArrayNotEmptyAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer){ }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-arraynotempty", "ERROR");
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext)
        {
            return validationContext.ModelMetadata.GetDisplayName();
        }
    }
}