using SynthetIQ.Function.Domain.Repository.API;

namespace SynthetIQ.Function.Domain.Value.Request
{
    public static class OpenAiRequestBaseExtensions
    {
        public static JsonSerializerOptions options { get; } = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles //doesn't add $id to json
        };

        public static string ToJson(this OpenAiRequestBase response)
        {
            return JsonSerializer.Serialize(response, options);
        }

        public static string FromJson(this OpenAiRequestBase response)
        {
            return JsonSerializer.Serialize(response, options);
        }

        public static bool CanDeserialize(this string json, Type toType)
        {
            var x = JsonSerializer.Deserialize(json, toType, options);
            return x != null;
        }

        /// <summary>
        /// Checks if all non-nullable value properties of an object are set to a non-default value
        /// </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static bool IsValid(this object obj)
        {
            // Get all properties of the object
            var properties = obj.GetType().GetProperties();

            // Loop through each property
            foreach (var property in properties)
            {
                // Check if the property is a value type and not nullable
                if (property.PropertyType.IsValueType && Nullable.GetUnderlyingType(property.PropertyType) == null)
                {
                    // Get the value of the property
                    var value = property.GetValue(obj);

                    // If the value is null or the default value, the property is invalid
                    if (value == null || value.Equals(Activator.CreateInstance(property.PropertyType)))
                    {
                        return false;
                    }
                }
            }

            // All non-nullable value properties are valid
            return true;
        }
    }

    [RegisterService]
    public class OpenAiRequestBase
    {
        public ApiRepository ApiRepo { get; private set; }

        public HttpHelpers HttpHelpers { get; private set; }

        public OpenAiRequestBase()
        {
        }
    }
}