namespace SynthetIQ.Functions.Domain.Value.Response
{
    internal class OpenAiFileResponse
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int bytes { get; set; }
        public int created_at { get; set; }
        public string filename { get; set; }
        public string purpose { get; set; }
    }
}