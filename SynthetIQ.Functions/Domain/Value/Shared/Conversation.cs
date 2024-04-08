namespace SynthetIQ.Functions.Domain.Value.Shared
{
    public sealed class Conversation
    {
        public string ConversationId { get; set; }
        public IList<ChatMessage> Messages { get; set; }

        public Conversation(string conversationId)
        {
            ConversationId = conversationId;
            Messages = new List<ChatMessage>();
        }

        public Conversation(string conversationId, List<ChatMessage> chatMessages)
        {
            ConversationId = conversationId;
            Messages = chatMessages;
        }
    }
}