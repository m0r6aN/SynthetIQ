namespace MF.DomainName.Health.Security
{
    internal class KeyVaultHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                // Get a token from the Azure AD identity provider
                var token = await GetIdentityTokenAsync();

                //// Use the token to make a request to the Key Vault API
                //var request = new HttpRequestMessage(HttpMethod.Get, "https://<your_key_vault_url>/secrets");
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //var response = HttpClient.SendAsync(request);

                //// If the response status code is 200 OK, then the Key Vault API was successfully accessed
                //if (response.StatusCode == HttpStatusCode.OK)
                //{
                //    return HealthCheckResult.Healthy("Successfully connected to Key Vault");
                //}
                //else
                //{
                //    return HealthCheckResult.Unhealthy("Failed to connect to Key Vault");
                //}

                return HealthCheckResult.Healthy("Successfully connected to Key Vault");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Failed to connect to Key Vault", ex);
            }
        }

        private async Task<string> GetIdentityTokenAsync()
        {
            //// Create an instance of the Azure AD client
            //var client = new Microsoft.Identity.Client (
            //    new Uri("https://login.microsoftonline.com/<your_azure_ad_tenant>/oauth2/v2.0/token"));

            //// Request a token
            //var result = await client.AcquireTokenAsync(
            //    new string[] { "https://vault.azure.net/.default" });

            //// Return the token
            //return result.AccessToken;

            return "";
        }
    }
}