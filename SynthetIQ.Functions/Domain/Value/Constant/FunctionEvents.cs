namespace SynthetIQ.Domain.Value.Constant
{
    internal static class FunctionEvents
    {
        public static string SynthetIQFunctionInvalidRequest { get; set; } = "SYNTHETIQ_FUNCTION_INAVALID_REQUEST";
        public static string SynthetIQFunctionRequestCompleted { get; set; } = "SYNTHETIQ_FUNCTION_REQUEST_COMPLETED";
        public static string SynthetIQFunctionRequestFailed { get; set; } = "SYNTHETIQ_FUNCTION_REQUEST_FAILED";
        public static string SynthetIQFunctionRequestStarted { get; set; } = "SYNTHETIQ_FUNCTION_REQUEST_STARTED";
        public static string OperationCanceled { get; set; } = "OPERAION_CANCELED";
        public static string KnownExceptionOccured { get; set; } = "KNOWN_EXCEPTION";
        public static string UnknownExceptionOccured { get; set; } = "UNKNOWN_EXCEPTION";
    }
}