namespace SynthetIQ.Utility.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ScanDomainAssemblies(this IServiceCollection services,
            ServiceDescriptorMergeStrategy mergeStrategy = ServiceDescriptorMergeStrategy.TryAdd)
        {
            // Scan SynthetIQ.Health assembly
            services.ScanAssembly(Assembly.Load("SynthetIQ.Function"), (Type _) => true, mergeStrategy);

            // Scan SynthetIQ.Interface assembly
            services.ScanAssembly(Assembly.Load("SynthetIQ.WebClients"), (Type _) => true, mergeStrategy);

            // Scan SynthetIQ.Utility assembly
            services.ScanAssembly(Assembly.Load("SynthetIQ.ConfigContext"), (Type _) => true, mergeStrategy);

            return services;
        }
    }
}