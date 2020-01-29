using Microsoft.AspNetCore.Mvc.ModelBinding;

using System.Linq;

namespace Vitae.Helper
{
    public static class Extensions
    {
        public static bool IsValid(this ModelStateDictionary modelState, string model)
        { 
            return modelState.FindKeysWithPrefix(model).All(k => k.Value.ValidationState == ModelValidationState.Valid);
        }
    }
}
