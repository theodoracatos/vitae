using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Library.Extensions
{
    public static class ClassExtensions
    {
        public static bool IsValid(this ModelStateDictionary modelState, string model)
        {
            return modelState.FindKeysWithPrefix(model).All(k => k.Value.ValidationState == ModelValidationState.Valid);
        }
    }
}
