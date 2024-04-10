namespace SynthetIQ.Functions.Domain.Value.Agents
{
    public sealed class Assistant : Agent
    {
        public Assistant(string agentName, string prompt)
        {
            Name = agentName;
            Role = "Assistant";
            Prompt = prompt;
            ImageUrl = "https://synthetiq.blob.core.windows.net/agents/assistant.png";
            Active = true;
            CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        public Assistant(Guid id, string capabilities)
        {
            Id = id;
            Name = "Assistant";
            Role = "Assistant";
            Prompt = "How can I help you today?";
            ImageUrl = "https://synthetiq.blob.core.windows.net/agents/assistant.png";
            Active = true;
            CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            Capabilities = capabilities;
        }
    }
}