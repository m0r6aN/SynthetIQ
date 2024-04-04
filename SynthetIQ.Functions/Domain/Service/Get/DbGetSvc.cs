using SynthetIQ.Function.Domain.Repository.API;

namespace SynthetIQ.Function.Services.Get.API
{
    // this will be picked up by ScanCurrentAssembly in Program.cs
    [RegisterService]
    public sealed class DbGetSvc
    {
        [InjectService]
        public ApiRepository ApiRepo { get; private set; }

        // use dependency injection to get a reference to the httpClientFactory and create an
        // instance of our named HttpClient that was defined in Program.cs
        public DbGetSvc(ApiRepository apiRepository) =>
            ApiRepo = apiRepository
                ?? throw new ArgumentNullException(nameof(apiRepository));

        // in almost all circumstances each service should have a single ExecuteAsync method
        public async Task<string> ExecuteAsync(IGetRequest request, CancellationToken ct, IFunctionResponse response)
        {
            ct.ThrowIfCancellationRequested();

            ApiRepo.ActionUrl = await request.ToUri();

            // get the response back as json from the repository
            string json = await ApiRepo.GetAsync(ct);

            // validate that the returned json can be deserialized into the response object
            return response.TryDeserialize(json) ? json :
                throw new FailedRequestException("Failed to deserialize the response from the API");
        }
    }
}