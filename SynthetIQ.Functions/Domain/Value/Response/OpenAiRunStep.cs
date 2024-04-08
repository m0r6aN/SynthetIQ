using SynthetIQ.Functions.Domain.Value.Shared;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    internal class OpenAiRunStep
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created_at { get; set; }
        public string run_id { get; set; }
        public string assistant_id { get; set; }
        public string thread_id { get; set; }
        public string type { get; set; }
        public string status { get; set; }
        public object cancelled_at { get; set; }
        public int completed_at { get; set; }
        public object expired_at { get; set; }
        public object failed_at { get; set; }
        public object last_error { get; set; }
        public Step_Details step_details { get; set; }
        public Usage usage { get; set; }
    }
}