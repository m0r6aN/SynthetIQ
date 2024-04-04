namespace SynthetIQ.Functions.Domain.Value.Response
{
    internal class OpenAiFineTuningJob
    {
        public string _object { get; set; }
        public string id { get; set; }
        public string model { get; set; }
        public int created_at { get; set; }
        public object fine_tuned_model { get; set; }
        public string organization_id { get; set; }
        public object[] result_files { get; set; }
        public string status { get; set; }
        public object validation_file { get; set; }
        public string training_file { get; set; }
    }
}