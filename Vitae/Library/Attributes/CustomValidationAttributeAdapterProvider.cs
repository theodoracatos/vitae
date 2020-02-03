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
            if (attribute is AgelimitAttribute agelimitAttribute)
            {
                return new agelimitAttributeAdapter(agelimitAttribute, stringLocalizer);
            }

            return baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }
    }
}