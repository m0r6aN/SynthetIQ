using SynthetIQ.Utility.Logging;
using SynthetIQ.Value.Constant;

using System.Diagnostics;

namespace SynthetIQ.Utility.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    [RegisterService]
    public class PerformanceMetricsAttribute : Attribute
    {
        //[InjectService]
        private readonly AppInsightsLogger Logger = new(AppInsightsLogLevel.Metrics);

        private readonly string _className;
        private readonly string _methodName;

        private Stopwatch stopwatch;

        public PerformanceMetricsAttribute(string className, string methodName)
        {
            _className = className;
            _methodName = methodName;
        }

        public void OnEntry()
        {
            Logger.Log($"Entering {_className}.{_methodName}", AppInsightsLogLevel.Info);
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void OnExit()
        {
            stopwatch.Stop();
            Logger.Log($"Exiting {_className}.{_methodName} in {stopwatch.ElapsedMilliseconds} ms", AppInsightsLogLevel.Info);
        }
    }
}