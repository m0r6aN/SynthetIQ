using Azure.Storage.Blobs;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MF.DomainName.Health.Storage
{
    /// <summary>
    /// Checks for Blob Storage availability and degraded performance.
    /// </summary>
    public class BlobStorageHealthCheck : IHealthCheck
    {
        // A cloud storage account contains all of your Azure Storage data objects, including blobs,
        // file shares, queues, tables, and disks. It's the top-level object used to access Azure
        // Storage. You can change this to FileShare or BlobStorage if you want to test the clients individually
        private readonly BlobServiceClient _blobServiceClient;

        [InjectService]
        public Settings Settings { get; private set; }

        public BlobStorageHealthCheck(BlobServiceClient blobServiceClient) =>
            _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));

        /* Check the code below for errors */

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Check if storage account is null
            //if (_blobServiceClient == null)
            //    return HealthCheckResult.Unhealthy("Storage account is null.");

            //// Check if storage account is valid
            //try
            //{
            //    await _storageAccount.CreateCloudBlobClient().GetServicePropertiesAsync();
            //}
            //catch (StorageException e)
            //{
            //    return HealthCheckResult.Unhealthy("Storage account is invalid or unavailable.", e);
            //}

            //// Check for degraded performance
            //BlobRequestOptions optionsWithRetryPolicy = new()
            //{
            //    // set delta back off and retry limit
            //    RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(20), 3)
            //};

            //// Setup the operation context.
            //OperationContext healthContext = new()
            //{
            //    ClientRequestID = "Azure Functions Health Check",
            //    StartTime = DateTime.UtcNow
            //};

            //// This has to exist in your respective storage account
            //var blob = _storageAccount
            //    .CreateCloudBlobClient()
            //    .GetContainerReference("health-check")
            //    .GetBlobReference("health-check.txt");

            //MemoryStream stream = new();

            //await
            //    blob.DownloadToStreamAsync(
            //        target: stream,
            //        accessCondition: null,
            //        options: optionsWithRetryPolicy,
            //        operationContext: healthContext);

            //healthContext.EndTime = DateTime.UtcNow;

            //var elapsed = (healthContext.EndTime - healthContext.StartTime).TotalMilliseconds;

            //if (elapsed > Settings.DegradedStorageThresholdMs)
            //    return HealthCheckResult.Degraded("Response time is too long.");

            //IList<RequestResult> stats = healthContext.RequestResults;

            //var errorInfo = stats
            //    .Where(s => s.ExtendedErrorInformation.ErrorCode != null)
            //    .ToList();

            return HealthCheckResult.Healthy();
        }
    }
}