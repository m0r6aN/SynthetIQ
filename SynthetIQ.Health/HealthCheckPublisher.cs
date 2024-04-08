namespace MF.DomainName.Health.Utility
{
    /// <summary>
    /// Periodically executes your health checks and calls PublishAsync with the result.
    ///
    /// HealthCheckPublisherOptions allow you to set the:
    /// Delay:  The initial delay applied after the app starts before executing
    /// IHealthCheckPublisher instances. The delay is applied once at startup and doesn't apply to
    /// later iterations. The default value is five seconds.
    ///
    /// Period: The period of IHealthCheckPublisher execution. The default value is 30 seconds.
    ///
    /// Predicate: If Predicate is null (default), the health check publisher service runs all
    /// registered health checks. To run a subset of health checks, provide a function that filters
    /// the set of checks.The predicate is evaluated each period.
    ///
    /// Timeout: The timeout for executing the health checks for all IHealthCheckPublisher instances.
    /// Use InfiniteTimeSpan to execute without a timeout. The default value is 30 seconds.
    /// </summary>
    [RegisterService]
    public class HealthCheckPublisher : IHealthCheckPublisher
    {
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            if (report.Status != HealthStatus.Healthy)
            {
                // TODO
            }
            return Task.CompletedTask;
        }
    }
}