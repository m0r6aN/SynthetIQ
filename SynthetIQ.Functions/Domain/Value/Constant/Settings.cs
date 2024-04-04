namespace SynthetIQ.Domain.Value.Constant
{
    internal static class Settings
    {
        public static string DomainDbConnection
           = Environment.GetEnvironmentVariable("DOMAIN_DB_CONNECTION");

        public static string DataExtractConnection
            = Environment.GetEnvironmentVariable("DATA_EXTRACT_CONNECTION");

        public static string DataWriteConnection
            = Environment.GetEnvironmentVariable("DATA_WRITE_CONNECTION");

        public static string OpenAIBaseUrl
            = Environment.GetEnvironmentVariable("OPENAI_BASE_URL");

        public static Uri KeyVaultUri
            = new Uri(Environment.GetEnvironmentVariable("KEY_VAULT_URI"));

        public static string OpenAIKey
            = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        public static string OpenAIModel
           = Environment.GetEnvironmentVariable("OPENAI_MODEL");

        public static string OpenAITemperature
           = Environment.GetEnvironmentVariable("OPENAI_TEMPERATURE");

        public static string OpenAIMaxTokens
           = Environment.GetEnvironmentVariable("OPENAI_MAX_TOKENS");
    }
}