namespace SynthetIQ.Functions.Domain.Value.Shared
{
    public sealed class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class Step_Details
    {
        public string Type { get; set; }
        public MessageCreation MessageCreation { get; set; }
    }

    public class MessageCreation
    {
        public string MessageId { get; set; }
    }

    public class Metadata
    {
    }

    public class Tool
    {
        public string Type { get; set; }
    }

    public class Content
    {
        public string Type { get; set; }
        public Text Text { get; set; }
    }

    public class Text
    {
        public string Value { get; set; }
        public object[] Annotations { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
        public object LogProbs { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Message
    {
        public string Role { get; set; } // e.g., "user" or "assistant"
        public string Content { get; set; }
    }
}