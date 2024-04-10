using SynthetIQ.DbContext.Context;

namespace SynthetIQ.Health.Database
{
    /// <summary>
    /// Checks for DbContext connectivity and degraded response times
    /// </summary>
    /// <typeparam name="TContext"> </typeparam>
    public sealed class DbContextHealthCheck<TContext> where TContext : Microsoft.EntityFrameworkCore.DbContext, IHealthCheck
    {
        private readonly SynthetIQContext _synthetIQDbContext;

        public DbContextHealthCheck(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            _synthetIQDbContext = (SynthetIQContext)dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                //bool canQuery = _synthetIQDbContext.Conversations.Any(i => i.Pkey != 0);

                stopwatch.Stop();

                if (stopwatch.ElapsedMilliseconds > 1000)
                {
                    return HealthCheckResult.Degraded();
                }

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy(ex.Message);
            }
        }
    }
}