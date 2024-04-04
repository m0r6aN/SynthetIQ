using System.Net.Http;

namespace MF.DomainName.Health.Http.Api
{
    public sealed class HttpClientHealthCheck : IHealthCheck
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientHealthCheck(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory
                ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        /// <summary>
        /// HealthCheck to test the HTTP client connectivity and degraded performance.
        /// </summary>
        /// <param name="httpClientFactory"> The http client factory. </param>
        /// <returns> HealthCheckResult. </returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient("APIM");

            var stopwatch = Stopwatch.StartNew();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("http://example.com");

                if (response.IsSuccessStatusCode)
                {
                    var elapsedTime = stopwatch.ElapsedMilliseconds;

                    if (elapsedTime > 1000)
                    {
                        return HealthCheckResult.Degraded();
                    }
                    else
                    {
                        return HealthCheckResult.Healthy();
                    }
                }
                else
                {
                    return HealthCheckResult.Unhealthy();
                }
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}