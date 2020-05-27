using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;

using Model.Attriutes;

using System;

namespace Model.Attributes
{
    public class DateGreaterThanAttributeAdapter : AttributeAdapterBase<DateGreaterThanAttribute>
    {
        public DateGreaterThanAttributeAdapter(DateGreaterThanAttribute attribute, IStringLocalizer stringLocalizer) : base(attribute, stringLocalizer)
        { }

        public override void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-dategreaterthan", GetErrorMessage(context));
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-untilnow", this.Attribute.UntilNow);
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-startyear", this.Attribute.StartYear);
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-startmonth", this.Attribute.StartMonth);
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-startday", this.Attribute.StartDay);
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-endmonth", this.Attribute.EndMonth);
            MergeAttribute(context.Attributes, "data-val-dategreaterthan-endday", this.Attribute.EndDay);
        }

        public override string GetErrorMessage(ModelValidationContextBase validationContext) => Attribute.GetErrorMessage();
    }
}
