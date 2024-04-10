using SynthetIQ.Function.Domain.Value.Response;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    public sealed class TagsResponse : ResponseBase, IFunctionResponse
    {
        public List<string> Tags { get; set; }

        public TagsResponse()
        {
        }

        public TagsResponse(List<string> tags)
        {
            Tags = tags;
        }

        public bool TryDeserialize(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return true;
            }

            var res = JsonConvert.DeserializeObject<TagsResponse>(json);
            return res != null;
        }
    }
}