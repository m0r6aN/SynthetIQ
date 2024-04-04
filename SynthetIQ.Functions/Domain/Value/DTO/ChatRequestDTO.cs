namespace SynthetIQ.Functions.Domain.Value.DTO
{
    /// <summary>
    /// This DTO can is used specifically for client requests, while our Conversation class remains
    /// clean and focused on its primary purpose.
    /// </summary>
    public class ChatRequestDTO
    {
        public string ConversationId { get; set; }
        public string NewMessage { get; set; }
    }
}