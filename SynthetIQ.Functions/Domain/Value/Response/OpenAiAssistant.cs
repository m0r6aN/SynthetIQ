using SynthetIQ.Functions.Domain.Value.Shared;

namespace SynthetIQ.Functions.Domain.Value.Response
{
    public class OpenAiAssistant
    {
        /// <summary>
        /// asst_abc123"
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// "assistant"
        /// </summary>
        public string _object { get; set; }

        /// <summary>
        /// 1698984975
        /// </summary>
        public int created_at { get; set; }

        /// <summary>
        /// "Math Tutor"
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// </summary>
        public object description { get; set; }

        /// <summary>
        /// "gpt-4"
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// "You are a personal math tutor. When asked a question, write and run Python code to
        /// answer the question.",
        /// </summary>
        public string instructions { get; set; }

        /// <summary>
        /// "type": "code_interpreter"
        /// </summary>
        public Tool[] tools { get; set; }

        public object[] file_ids { get; set; }
        public Metadata metadata { get; set; }
    }
}