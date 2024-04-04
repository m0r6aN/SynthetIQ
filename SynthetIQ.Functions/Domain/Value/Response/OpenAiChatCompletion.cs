using SynthetIQ.Functions.Domain.Value.Shared;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    public sealed class OpenAiChatCompletion : IFunctionResponse
    {
        /// <summary>
        /// "chatcmpl-123"
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// "chat.completion"
        /// </summary>
        public string _object { get; set; }

        /// <summary>
        /// 1677652288
        /// </summary>
        public int created { get; set; }

        /// <summary>
        /// "gpt-3.5-turbo-0125"
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// "fp_44709d6fcb"
        /// </summary>
        public string system_fingerprint { get; set; }

        /// <summary> "index": 0,
        // "message": { "role": "assistant", "content": "Hello there, how may I assist you today?", },
        /// </summary>
        public Choice[] choices { get; set; }

        public Usage usage { get; set; }

        public bool TryDeserialize(string json)
        {
            throw new NotImplementedException();
        }
    }
}