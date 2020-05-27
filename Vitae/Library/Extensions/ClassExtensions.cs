using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Library.Extensions
{
    public static class ClassExtensions
    {
        public static bool IsValid(this ModelStateDictionary modelState, string model)
        {
            return modelState.FindKeysWithPrefix(model).All(k => k.Value.ValidationState == ModelValidationState.Valid);
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}