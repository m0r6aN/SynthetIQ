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

        public async Task<bool> CreateAssistantAsync(string name, int modelId,
            string description, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            Assistant assistant = new Assistant
            {
                Name = name,
                Description = description,
                ModelId = modelId
            };

            var response = await DbRepository.UpsertEntityAsync(assistant, ct);
            return response; // Assuming the API returns an object with an ID field
        }

        public async Task UpdateAssistantAsync(string assistantId, string name = null, string model = null, Dictionary<string, string> metadata = null)
        {
            throw new NotImplementedException();
            //var assistant = await DbRepository.GetEntityAsync<Assistant>(assistantId, ct);

            //var payload = new
            //{
            //    name = name,
            //    model = model,
            //    metadata = metadata
            //}.Where(p => p.Value != null).ToDictionary(p => p.Key, p => p.Value); // Remove null properties

            //var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PatchAsync($"{_baseUrl}assistants/{assistantId}", content);
            //response.EnsureSuccessStatusCode();
        }

        public async Task<string> InvokeAssistantAsync(string assistantId, string input)
        {
            throw new NotImplementedException();
            //var payload = new
            //{
            //    inputs = new { text = input }
            //};

            //var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            //var response = await _httpClient.PostAsync($"{_baseUrl}assistants/{assistantId}/completions", content);
            //response.EnsureSuccessStatusCode();

            //var responseContent = await response.Content.ReadAsStringAsync();
            //dynamic result = JsonConvert.DeserializeObject(responseContent);
            //return result.choices[0].message.content; // Assuming the API returns a structure with choices and content
        }
    }
}