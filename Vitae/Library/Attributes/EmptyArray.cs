using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class RequiredArray : ValidationAttribute, IClientModelValidator
    {
        public override bool IsValid(object value)
        {
            var list = value as string[];
            if (list != null)
            {
                return list.Length > 0;
            }
            return false;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            MergeAttribute(context.Attributes, "data-val", "true");
            var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
            MergeAttribute(context.Attributes, "data-val-emptyarray", errorMessage);
        }

        private bool MergeAttribute(
            IDictionary<string, string> attributes,
            string key,
            string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}