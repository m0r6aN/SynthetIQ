namespace SynthetIQ.Function.Domain.Repository.API
{
    /// <summary>
    /// Generic API Repository for making HTTP requests for all common verbs
    /// </summary>

    [RegisterService]
    public sealed class OpenAiRepository : IApiRepository
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Supplied by the IRequest object in the calling service
        /// </summary>
        public Uri ActionUrl { get; set; }

        public OpenAiRepository(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory.CreateClient("OpenAI")
                ?? throw new ArgumentNullException(nameof(httpClientFactory));

        /// <summary>
        /// </summary>
        /// <param name="ct"> </param>
        /// <returns> </returns>
        /// <exception cref="FailedRequestException"> </exception>
        public async Task<string> GetAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead;
            using HttpRequestMessage request = new(HttpMethod.Get, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, completionOption, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(ct) ?? string.Empty;
        }

        public async Task<string> PostAsync(object content, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ActionUrl)
            {
                Content = jsonContent
            };
            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync(ct) ?? string.Empty;
        }

        public async Task<string> PatchAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync() ?? string.Empty;
        }

        public async Task<string> PutAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync() ?? string.Empty;
        }

        public async Task<string> DeleteAsync(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, ActionUrl);
            using HttpResponseMessage response = await _httpClient.SendAsync(request, ct);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync() ?? string.Empty;
        }
    }
}