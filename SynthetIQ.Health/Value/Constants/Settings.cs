namespace MF.DomainName.Health.Value.Constants
{
    [RegisterService]
    public sealed class Settings
    {
        /// <summary>
        /// Storage response times over this threshold are considered degraded
        /// </summary>
        public int DegradedStorageThresholdMs { get; set; } = 1000;

        public string DeploymentDirectory { get; set; }
            = Path.Combine(Environment.CurrentDirectory, "..\\APIM");

        public Settings()
        { }
    }
}