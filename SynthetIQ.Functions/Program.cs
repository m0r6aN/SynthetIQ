using Azure.Identity;

using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;

using Quickwire;

using SynthetIQ.Functions.Domain.Value;

namespace SynthetIQ.Function
{
    class Program
    {
        public static void Main()
        {
            Environment.SetEnvironmentVariable("DOTNET_DISABLE_USER_SECRETS", "1");

            var builder = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var host = new HostBuilder()
                 .ConfigureFunctionsWorkerDefaults()
                 .ConfigureServices((context, services) =>
                   {
                       services.AddApplicationInsightsTelemetryWorkerService();

                       services.AddSingleton<OpenAIService>(_ =>
                       {
                           return new OpenAIService(new OpenAiOptions
                           {
                               ApiKey = "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",
                               ApiVersion = "v1",
                               BaseDomain = "https://api.openai.com",
                               ProviderType = ProviderType.OpenAi,
                               Organization = "org-3puba41sPvY7cGvHOxHrDuXE"
                           });
                       });

                       // Add Azure clients
                       services.AddAzureClients(clientBuilder =>
                       {
                           clientBuilder.AddBlobServiceClient(connectionString: "UseDevelopmentStorage=true");
                       });

                       // Register all services in the current assembly decorated with the
                       // RegisterService attribute Use TryAdd to avoid overwriting or duplicating
                       // existing services
                       services.ScanCurrentAssembly();

                       services.Configure<LoggerFilterOptions>(options =>
                       {
                           LoggerFilterRule toRemove = options.Rules.FirstOrDefault(rule => rule.ProviderName
                               == "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

                           options.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);

                           if (toRemove is not null)
                           {
                               options.Rules.Remove(toRemove);
                           }
                       });

                       services.AddHttpClient("OpenAI", (serviceProvider, httpClient) =>
                       {
                           // Use serviceProvider to resolve IOptions<OpenAiOptions>
                           var openAiOptions = serviceProvider.GetRequiredService<IOptions<OpenAiOptions>>().Value;

                           httpClient.BaseAddress = new Uri(openAiOptions.BaseDomain);
                           httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                           httpClient.DefaultRequestHeaders.Add("User-Agent", "SynthetIQ.Function");
                           httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {openAiOptions.ApiKey}"); // Use the property from options
                                                                                                                    //httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", $"assistants=v1");
                       })
                        .SetHandlerLifetime(TimeSpan.FromMinutes(5)); // this helps mitigate SNAT (Socket / Port) exhaustion
                   })

                .Build();

            host.Run();
        }
    }
}