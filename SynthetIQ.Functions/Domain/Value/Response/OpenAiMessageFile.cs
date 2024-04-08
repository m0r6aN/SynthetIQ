namespace SynthetIQ.Functions.Domain.Value.Response
{
    public sealed class OpenAiMessageFile
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int CreatedAt { get; set; }
        public string MessageId { get; set; }
        public string FileId { get; set; }
    }
}