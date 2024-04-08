using SynthetIQ.Functions.Domain.Value.Shared;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    internal class OpenAiCompletionResponse
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Usage usage { get; set; }
        public Choice[] choices { get; set; }
    }
}