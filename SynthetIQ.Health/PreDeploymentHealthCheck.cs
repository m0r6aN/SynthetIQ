namespace MF.DomainName.Health
{
    public class BicepFileHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Check that all endpoints have a corresponding .bicep file
            var endpoints = GetEndpoints();
            var bicepFiles = GetBicepFiles();
            foreach (string endpoint in endpoints)
            {
                if (!bicepFiles.Contains(endpoint))
                {
                    return HealthCheckResult.Unhealthy("Endpoint " + endpoint + " is missing a corresponding .bicep file!");
                }
            }

            // Check that all endpoints are listed in main.bicep
            var mainBicep = await GetMainBicepContent();
            foreach (string endpoint in endpoints)
            {
                if (!mainBicep.Contains(endpoint))
                {
                    return HealthCheckResult.Unhealthy("Endpoint " + endpoint + " is missing from main.bicep!");
                }
            }

            return HealthCheckResult.Healthy();
        }

        private List<string> GetEndpoints()
        {
            List<string> endpoints = new List<string>();
            // Populate endpoints list from Azure Function endpoints
            return endpoints;
        }

        private List<string> GetBicepFiles()
        {
            List<string> bicepFiles = new List<string>();
            // Populate bicepFiles list from .bicep files in project
            return bicepFiles;
        }

        private async Task<string> GetMainBicepContent()
        {
            // Get content of main.bicep
            var content = await File.ReadAllTextAsync("main.bicep");
            return content;
        }
    }
}