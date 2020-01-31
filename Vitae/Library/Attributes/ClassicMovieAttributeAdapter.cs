using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Library.Attributes
{
    public class ClassicMovieAttribute : ValidationAttribute
    {
        public ClassicMovieAttribute(int year)
        {
            Year = year;
        }

        public int Year { get; }

        public string GetErrorMessage() =>
            $"Classic movies must have a release year no later than {Year}.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            return new ValidationResult("Not good");
            
            //return ValidationResult.Success;
        }
    }

    public class ClassicMovieAttributeAdapter : AttributeAdapterBase<ClassicMovieAttribute>
    {
        public ClassicMovieAttributeAdapter(ClassicMovieAttribute attribute,
            IStringLocalizer stringLocalizer)
            : base(attribute, stringLocalizer)
        {

        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-classicmovie", GetErrorMessage(context));

            var year = Attribute.Year.ToString(CultureInfo.InvariantCulture);
            MergeAttribute(context.Attributes, "data-val-classicmovie-year", year);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) =>
            Attribute.GetErrorMessage();
    }

    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider =
            new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute,
            IStringLocalizer stringLocalizer)
        {
            if (attribute is ClassicMovieAttribute classicMovieAttribute)
            {
                return new ClassicMovieAttributeAdapter(classicMovieAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
