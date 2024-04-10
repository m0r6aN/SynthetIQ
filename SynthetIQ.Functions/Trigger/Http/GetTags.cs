using SynthetIQ.Function.Domain.Value.Request;
using SynthetIQ.Function.Services.Get.API;
using SynthetIQ.Functions.Domain.Value.Enum;
using SynthetIQ.Functions.Domain.Value.Response;

namespace SynthetIQ.Function.Trigger.Http
{
    public sealed class GetTags
    {
        [InjectService]
        public ApiGetSvc ApiGetSvc { get; private set; }

        public GetTags(ApiGetSvc apiGetSvc)
        {
            ApiGetSvc = apiGetSvc ?? throw new ArgumentNullException(nameof(apiGetSvc));
        }

        [Function(nameof(GetTags))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
            FunctionContext executionContext,
            int entityId,
            EntityType entityType,
            string search = "",
            CancellationToken hostCancellationToken = default)
        {
            var logger = executionContext.GetLogger(nameof(GetTags));
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
                List<string> searches = search.Split(',').ToList();
                var request = new TagRequest(entityId, entityType, searches);
                var response = new TagsResponse();

                //var tagsResponse = await Tagse.ExecuteAsync(request, response, ct);
                var functionResponse = req.CreateResponse(HttpStatusCode.OK);
                await functionResponse.WriteAsJsonAsync(""); //tagsResponse
                return functionResponse;
            }
            catch (System.Exception ex)
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