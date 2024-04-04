namespace SynthetIQ.Utility.Logging
{
    [RegisterService]
    public class AppInsightsLogger
    {
        private static readonly object _lock = new object();
        private static TelemetryClient? _telemetryClient = new();
        private AppInsightsLogLevel _logLevel;

        public AppInsightsLogger(AppInsightsLogLevel logLevel)
        {
            _logLevel = logLevel;
        }

        public static void Init(TelemetryConfiguration config)
        {
            _telemetryClient = new TelemetryClient(config);
        }

        public void Log(string message, AppInsightsLogLevel logLevel)
        {
            if (logLevel < _logLevel) return;
            lock (_lock)
            {
                switch (logLevel)
                {
                    case AppInsightsLogLevel.Trace:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Verbose);
                        break;

                    case AppInsightsLogLevel.Debug:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
                        break;

                    case AppInsightsLogLevel.Info:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);
                        break;

                    case AppInsightsLogLevel.Warning:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Warning);
                        break;

                    case AppInsightsLogLevel.Error:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error);
                        break;

                    case AppInsightsLogLevel.Critical:
                        _telemetryClient.TrackTrace(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Critical);
                        break;
                }
            }
        }
    }
}