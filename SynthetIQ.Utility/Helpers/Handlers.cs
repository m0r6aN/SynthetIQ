namespace SynthetIQ.Utility.Helpers
{
    /// <summary>
    /// This code uses a single catch block to handle all the exceptions and a switch statement to
    /// decide which exception type should be wrapped in a FailedRequestException or
    /// OperationCanceledException. The HandleException method always returns false, so the catch
    /// block will never execute, but the appropriate exception will be thrown from within the method.
    ///
    /// Be sure to set the original exception as the InnerException of the new exception to preserve
    /// the call stack and make debugging easier.
    /// </summary>
    public static class ExceptionHandlers
    {
        public static bool HandleHttpClientExceptions(string method, System.Exception ex, HttpStatusCode statusCode)
        {
            string errorMessage = $"{method} returned {statusCode} with error: {ex.Message}";

            switch (ex)
            {
                case ArgumentNullException:
                case InvalidOperationException:
                case HttpRequestException:
                    throw new FailedRequestException(errorMessage, ex);
                case TaskCanceledException:
                    throw new OperationCanceledException(errorMessage, ex);
                default:
                    return false; // Unhandled exception types will propagate up the call stack.
            }
        }
    }
}