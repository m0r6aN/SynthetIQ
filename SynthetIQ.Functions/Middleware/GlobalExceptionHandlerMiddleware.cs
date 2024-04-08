namespace SynthetIQ.Function.Middleware
{
    public sealed class GlobalExceptionHandlerMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly ILogger _logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            CancellationToken ct = context.CancellationToken;

            try
            {
                // Invoke the next middleware in the pipeline
                await next(context);
            }
            catch (OperationCanceledException ex)
            {
                var httpResponse = context.GetHttpResponseData();

                if (ct.IsCancellationRequested)
                {
                    // The operation was canceled by the client
                    httpResponse.StatusCode = HttpStatusCode.RequestTimeout;
                    var message = $"Operation canceled (client cancellation): {ex.Message}";
                    _logger.LogWarning(message);
                }
                else
                {
                    // The operation was canceled for an unknown reason
                    httpResponse.StatusCode = HttpStatusCode.InternalServerError;
                    var message = $"Operation canceled (unknown reason): {ex.Message}";
                    _logger.LogWarning(message);
                }

                // Write the error message to the response body
                var buffer = Encoding.UTF8.GetBytes(httpResponse.StatusCode.ToString());
                await httpResponse.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                // Handle the exception
                HandleException(ex, context.GetHttpResponseData());
                throw;
            }
        }

        private void HandleException(Exception ex, HttpResponseData httpResponse)
        {
            string errorMessage;
            httpResponse.StatusCode = HttpStatusCode.InternalServerError;

            switch (ex)
            {
                case ArgumentNullException anex:
                case InvalidOperationException ioex:
                case HttpRequestException rex:
                    errorMessage = $"An error occurred: {ex.Message}";
                    break;

                default:
                    errorMessage = "An unknown error occurred.";
                    break;
            }

            // Write the error message to the response body
            var buffer = Encoding.UTF8.GetBytes(errorMessage);
            httpResponse.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}