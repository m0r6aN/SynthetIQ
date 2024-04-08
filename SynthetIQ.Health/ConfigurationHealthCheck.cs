namespace MF.DomainName.Health.Configuration
{
    /// <summary>
    /// Checks for missing environment variable values
    /// </summary>
    public class EnvironmentHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _config;

        public EnvironmentHealthCheck(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Checks for missing environment variable values and returns a list of keys with missing values
        /// </summary>
        /// <param name="context">          </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            string missing = string.Empty;

            // Use the Keys from local.settings.json to check if the same key is present in the
            // Azure Functions configuration
            foreach (var setting in _config.AsEnumerable())
            {
                if (!string.IsNullOrEmpty(setting.Value))
                {
                    if (Environment.GetEnvironmentVariable(setting.Key) == null)
                    {
                        missing += $"{setting.Key}, ";
                    }
                }
            }

            if (!string.IsNullOrEmpty(missing))
            {
                return HealthCheckResult.Unhealthy($"HEALTH CHECK FAILED!: The following config keys are missing values {missing[..^2]} ");
            }

            return HealthCheckResult.Healthy();
        }
    }
}