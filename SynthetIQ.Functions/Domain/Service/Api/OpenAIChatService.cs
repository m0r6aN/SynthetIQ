namespace SynthetIQ.Functions.Domain.Service.Api
{
    [RegisterService(ServiceLifetime.Singleton)]
    public class OpenAIChatService
    {
        private OpenAIService _openAiService;
        private BlobServiceClient _blobServiceClient;

        private const string ContainerName = "open-ai-conversations";

        public OpenAIChatService(OpenAIService openAiService, BlobServiceClient blobServiceClient)
        {
            _openAiService = openAiService ?? throw new ArgumentNullException(nameof(openAiService));
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));

            _blobServiceClient.GetBlobContainerClient(ContainerName).CreateIfNotExists();
        }

        public async Task<string> ChatWithOpenAIAsync(ChatRequestDTO requestData, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // Retrieve existing conversation state or start a new one
            var conversation = await RetrieveConversationState(requestData.ConversationId);

            conversation.Messages.Add(new ChatMessage(StaticValues.ChatMessageRoles.User, requestData.NewMessage));

            try
            {
                var modelToUse = SelectModelBasedOnContext();//conversation

                var completionResult = _openAiService.ChatCompletion.CreateCompletionAsStream(new ChatCompletionCreateRequest
                {
                    Messages = conversation.Messages,
                    MaxTokens = 500,
                    Model = modelToUse // Use the dynamically selected model
                });

                await foreach (var completion in completionResult)
                {
                    if (completion.Successful)
                    {
                        conversation.Messages.Add(new ChatMessage(StaticValues.ChatMessageRoles.Assistant, completion.Choices.First().Message.Content));
                    }
                    else
                    {
                        // Handle error or add an error message to the list
                        conversation.Messages.Add(new ChatMessage(StaticValues.ChatMessageRoles.Tool, "Error: " + (completion.Error?.Message ?? "Unknown Error")));
                    }
                }

                // Save updated conversation state
                await SaveConversationState(conversation);

                return string.Join("\n", conversation.Messages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<Conversation> RetrieveConversationState(string conversationId)
        {
            try
            {
                var blobClient = _blobServiceClient.GetBlobContainerClient(ContainerName).GetBlobClient(conversationId);
                if (await blobClient.ExistsAsync())
                {
                    var response = await blobClient.DownloadContentAsync();
                    var content = response.Value.Content.ToString();
                    return JsonConvert.DeserializeObject<Conversation>(content);
                }

                var messages = new List<ChatMessage>
                {
                    new ChatMessage(StaticValues.ChatMessageRoles.User, "You are a helpful assistant")
                };

                return new Conversation(conversationId, messages);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task SaveConversationState(Conversation conversation)
        {
            try
            {
                var blobClient = _blobServiceClient.GetBlobContainerClient(ContainerName).GetBlobClient(conversation.ConversationId);
                var content = JsonConvert.SerializeObject(conversation.Messages);
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes(content));
                await blobClient.UploadAsync(ms, overwrite: true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private string SelectModelBasedOnContext() //Conversation conversation
        {
            // Placeholder logic to select a model based on the conversation context This could be
            // replaced with more complex logic involving multiple factors
            return Models.Gpt_3_5_Turbo; // Default model, adjust as necessary
        }
    }
}