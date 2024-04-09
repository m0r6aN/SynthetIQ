using SynthetIQ.Functions.Domain.Value.Response;

namespace SynthetIQ.Function.Trigger.Http
{
    public sealed class GetTagsRequest
    {
        [InjectService]
        public ApiGetSvc ApiGetSvc { get; private set; }

        public GetTagsRequest(ApiGetSvc apiGetSvc)
        {
            ApiGetSvc = apiGetSvc ?? throw new ArgumentNullException(nameof(apiGetSvc));
        }

        [Function(nameof(GetTagsRequest))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext,
            string hints = "",
            CancellationToken hostCancellationToken = default)
        {
            var logger = executionContext.GetLogger("OpenAiCompletionFunction");
            logger.LogInformation(FunctionEvents.SynthetIQFunctionRequestStarted);

            // Async functions receive 2 cancellation tokens. One from the calling client and one
            // from the host (APIM). Get a linked ct source that will throw an
            // OperationCanceledException if it receives a cancellation request from either.
            var lts = CancellationTokenSource.CreateLinkedTokenSource(
                hostCancellationToken,
                executionContext.CancellationToken);

            int timeoutMS = 20000;
            lts.CancelAfter(timeoutMS);
            CancellationToken ct = lts.Token;

            // Throw and catch an exception if cancellation is requested
            ct.ThrowIfCancellationRequested();

            try
            {
                IGetRequest request = new Domain.Value.Request.GetTagsRequest(hints);
                var response = new TagsResponse();

                var chatResponses = await ApiGetSvc.ExecuteAsync(request, response, ct);
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