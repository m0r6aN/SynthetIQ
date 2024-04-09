using SynthetIQ.DbContext.Models;
using SynthetIQ.Function.Domain.Repository.DB;

namespace SynthetIQ.Functions.Domain.Value.Helpers
{
    public class AssistantManager
    {
        [InjectService]
        public DbRepository DbRepository { get; private set; }

        public AssistantManager(DbRepository dbRepository)
        {
            DbRepository = dbRepository ?? throw new ArgumentNullException(nameof(dbRepository));
        }

        public async Task<string> CreateAssistantAsync(string name, string model, Dictionary<string, string> metadata = null, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Assistant assistant = new Assistant { Name = name, Model = model, Metadata = metadata };

            var result = await DbRepository.UpsertEntityAsync(assistant, ct);

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent);
            return result.id; // Assuming the API returns an object with an ID field
        }

        public async Task UpdateAssistantAsync(string assistantId, string name = null, string model = null, Dictionary<string, string> metadata = null)
        {
            var assistant =

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