using System.Net.Http.Headers;

namespace SynthetIQ.Functions.Domain.Value.Helpers
{
    public class AssistantManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.openai.com/v1/";
        private readonly string _apiKey;

        public AssistantManager(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient
            {
                DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", _apiKey)
            }
            };
        }

        public async Task<string> CreateAssistantAsync(string name, string model, Dictionary<string, string> metadata = null)
        {
            var payload = new
            {
                name = name,
                model = model,
                metadata = metadata
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}assistants", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);
            return result.id; // Assuming the API returns an object with an ID field
        }

        public async Task UpdateAssistantAsync(string assistantId, string name = null, string model = null, Dictionary<string, string> metadata = null)
        {
            var payload = new
            {
                name = name,
                model = model,
                metadata = metadata
            }.Where(p => p.Value != null).ToDictionary(p => p.Key, p => p.Value); // Remove null properties

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PatchAsync($"{_baseUrl}assistants/{assistantId}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> InvokeAssistantAsync(string assistantId, string input)
        {
            var payload = new
            {
                inputs = new { text = input }
            };

            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}assistants/{assistantId}/completions", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);
            return result.choices[0].message.content; // Assuming the API returns a structure with choices and content
        }
    }
}