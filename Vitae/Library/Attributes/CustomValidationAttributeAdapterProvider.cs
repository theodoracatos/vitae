using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Library.Attributes
{
    public class CustomValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is ArrayNotEmptyAttribute arrayNotEmptyAttribute)
            {
                return new ArrayNotEmptyAttributeAdapter(arrayNotEmptyAttribute, stringLocalizer);
            }

            if (attribute is ClassicMovieAttribute classicMovieAttribute)
            {
                return new ClassicMovieAttributeAdapter(classicMovieAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}
