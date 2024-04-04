using Azure.Core;
using Azure;

using OpenAI;
using Azure.AI.OpenAI;

namespace SynthetIQ.Function.Services.Get.API
{
    [RegisterService]
    public sealed class OpenAIApiSvc
    {
        private readonly HttpClient _httpClient;
        private HttpStatusCode statusCode = HttpStatusCode.OK;

        /// <summary>
        /// Supplied by the IRequest object in the calling service
        /// </summary>
        public Uri ActionUrl { get; set; }

        public OpenAIApiSvc(IHttpClientFactory httpClientFactory) =>
            _httpClient = httpClientFactory.CreateClient("OpenAI")
                ?? throw new ArgumentNullException(nameof(httpClientFactory));

        public async Task<ChatCompletions> ExecuteAsync(string prompt, IEnumerable<string> userMessages, CancellationToken ct)
        {
            string openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            var client = new OpenAIClient(new Uri("Your Azure OpenAI Endpoint"), new AzureKeyCredential(openAiKey));

            // Initialize chat completion request options with your specific requirements
            var chatCompletionsOptions = new ChatCompletionsOptions("Your Deployment Name",
                // Assuming 'prompt' and 'userMessages' are structured to fit your specific use case
               Messages = new List<ChatRequestMessage>
                {
                    new ChatRequestSystemMessage("System message if any"),
                    new ChatRequestUserMessage(prompt) // Your initial prompt
                    // You might want to add userMessages here if needed
                }
            };

            // Add user messages to the chat completion options
            foreach (var message in userMessages)
            {
                chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(message));
            }

            // Execute the chat completion request
            var response = await client.GetChatCompletionsAsync(chatCompletionsOptions, ct);

            return response.Value; // Make sure to handle this response according to your application's needs
        }



        //public async IAsyncEnumerable<string> ExecuteStreamAsync(string prompt, [EnumeratorCancellation] CancellationToken ct)
        //{
        //    ct.ThrowIfCancellationRequested();

        // var completionResult = _openAiSvc.Completions.CreateCompletionAsStream(new
        // CompletionCreateRequest() { Prompt = prompt //MaxTokens = 500, // optional //Model =
        // Models.ChatGpt3_5Turbo //optional });

        // await foreach (var completion in completionResult) // Ensuring it respects cancellation
        // requests. { if (completion.Successful) { var choiceText =
        // completion.Choices.FirstOrDefault()?.Text; if (choiceText != null) { yield return
        // choiceText; } } else { if (completion.Error == null) { throw new Exception("Unknown
        // Error"); }

        //            Console.WriteLine($"{completion.Error.Code}: {completion.Error.Message}");
        //        }
        //    }
        //}

        // FALLBACK
        //public async Task<string> ExecuteAsync(ChatCompletionCreateRequest request, IFunctionResponse response, CancellationToken ct)
        //{
        //    ct.ThrowIfCancellationRequested();

        // var completionResult = await _openAiSvc.ChatCompletion.CreateCompletion(request, modelId:
        // null, ct);

        // if (completionResult.Successful) {
        // Console.WriteLine(completionResult.Choices.First().Message.Content); }

        // string json = completionResult.Choices.First().Message.Content;

        //    // The response object ensures that the response is valid before it's returned to the trigger
        //    return response.TryDeserialize(json) ? json : throw new FailedRequestException("Failed to deserialize the response from the API");
        //}
    }
}