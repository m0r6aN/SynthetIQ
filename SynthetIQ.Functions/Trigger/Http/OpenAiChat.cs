using SynthetIQ.Functions.Domain.Service.Api;
using SynthetIQ.Functions.Domain.Value.DTO;

namespace SynthetIQ.Function.Trigger.Http
{
    public sealed class OpenAiFunction
    {
        [InjectService]
        public OpenAIChatService ChatService { get; private set; }

        public OpenAiFunction(OpenAIChatService chatService)
        {
            ChatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        [Function(nameof(OpenAiFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext,
            CancellationToken hostCancellationToken = default)
        {
            var logger = executionContext.GetLogger("OpenAiCompletionFunction");
            logger.LogInformation(FunctionEvents.SynthetIQFunctionRequestStarted);

            // Async functions receive 2 cancellation tokens. One from the calling client and one
            // from the host (APIM). Get a linked ct source that will throw an
            // OperationCanceledException if it receives a cancellation request from either.
            var lts = CancellationTokenSource.CreateLinkedTokenSource(hostCancellationToken, executionContext.CancellationToken);
            int timeoutMS = 20000;
            lts.CancelAfter(timeoutMS);
            CancellationToken ct = lts.Token;

            // Throw and catch an exception if cancellation is requested
            ct.ThrowIfCancellationRequested();

            try
            {
                // Parse the request body
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync(ct);
                var requestData = JsonConvert.DeserializeObject<ChatRequestDTO>(requestBody);

                if (string.IsNullOrEmpty(requestData?.ConversationId))
                {
                    var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badRequest.WriteStringAsync("No ConversationId!");
                    return badRequest;
                }

                if (string.IsNullOrEmpty(requestData?.NewMessage))
                {
                    var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                    await badRequest.WriteStringAsync("No prompt!");
                    return badRequest;
                }

                // Pass the conversationId to ChatWithOpenAIAsync
                var chatResponses = await ChatService.ChatWithOpenAIAsync(requestData, ct);
                var functionResponse = req.CreateResponse(HttpStatusCode.OK);
                await functionResponse.WriteAsJsonAsync(chatResponses);
                return functionResponse;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, FunctionEvents.SynthetIQFunctionRequestFailed);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
            finally
            {
                // Dispose of the CancellationTokenSource. Important!
                lts.Dispose();
            }
        }
    }
}