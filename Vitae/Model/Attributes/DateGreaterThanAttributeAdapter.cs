using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using Model.Attriutes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Attributes
{
    public class DateGreaterThanAttributeAdapter : AttributeAdapterBase<DateGreaterThanAttribute>
    {
        public DateGreaterThanAttributeAdapter(DateGreaterThanAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        { }

        public override void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-datecompare", GetErrorMessage(context));
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) => Attribute.GetErrorMessage();
    }
}
