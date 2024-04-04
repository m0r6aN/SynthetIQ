using SynthetIQ.Function.Domain.Value.Response;

public class Metadata
{
}

namespace SynthetIQ.Functions.Domain.Value.Response
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public sealed class OpenAiThreadResponse : ResponseBase, IFunctionResponse
    {
        /// <summary>
        /// "file-abc123"
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// "assistant.file"
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// 1699055364
        /// </summary>
        public int CreatedAt { get; set; }

        /// <summary>
        /// "asst_abc123"
        /// </summary>
        public string AssistantId { get; set; }

        public Metadata MetaData { get; set; }

        public bool TryDeserialize(string json)
        {
            throw new NotImplementedException();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}