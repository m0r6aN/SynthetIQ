namespace SynthetIQ.Functions.Domain.Value
{
    // Use the Options pattern with dependency injection for managing configurations and settings
    public class OpenAiOpts
    {
        public string ApiKey { get; set; }
        public string ApiVersion { get; set; }
        public string BaseDomain { get; set; }
        public string ProviderType { get; set; }
        public string Organization { get; set; }

        public OpenAiOpts()
        {
        }
    }
}