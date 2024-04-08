using SynthetIQ.Functions.Domain.Value.Shared;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    internal class OpenAiRun
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created_at { get; set; }
        public string assistant_id { get; set; }
        public string thread_id { get; set; }
        public string status { get; set; }
        public int started_at { get; set; }
        public object expires_at { get; set; }
        public object cancelled_at { get; set; }
        public object failed_at { get; set; }
        public int completed_at { get; set; }
        public object last_error { get; set; }
        public string model { get; set; }
        public object instructions { get; set; }
        public Tool[] tools { get; set; }
        public object[] file_ids { get; set; }
        public Metadata metadata { get; set; }
        public Usage usage { get; set; }
        public int temperature { get; set; } = 1;
    }
}