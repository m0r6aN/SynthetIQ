namespace SynthetIQ.Function.Domain.Value.Response
{
    public static class ResponseBaseExtensions
    {
        public static string ToJson(this ResponseBase response)
        {
            return JsonSerializer.Serialize(response);
        }

        public static string FromJson(this ResponseBase response)
        {
            return JsonSerializer.Serialize(response);
        }

        public static bool CanDeserialize(this string json, Type toType)
        {
            var x = JsonSerializer.Deserialize(json, toType);
            return x != null;
        }
    }

    public class ResponseBase
    {
    }
}