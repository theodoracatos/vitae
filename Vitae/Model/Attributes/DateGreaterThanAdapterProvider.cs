using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Localization;

using Model.Attriutes;

using System.ComponentModel.DataAnnotations;

namespace Model.Attributes
{
    public class DateGreaterThanAdapterProvider : IValidationAttributeAdapterProvider
    {
        private readonly IValidationAttributeAdapterProvider _baseProvider = new ValidationAttributeAdapterProvider();

        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            if (attribute is DateGreaterThanAttribute)
            {
                return new DateGreaterThanAttributeAdapter(attribute as DateGreaterThanAttribute, stringLocalizer);
            }
            else
            {
                return _baseProvider.GetAttributeAdapter(attribute, stringLocalizer);
            }
        }
    }
}