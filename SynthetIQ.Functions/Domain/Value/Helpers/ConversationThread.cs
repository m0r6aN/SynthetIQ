namespace SynthetIQ.Functions.Domain.Value.Helpers
{
    public class ConversationThread
    {
        public string ThreadId { get; set; }
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}