using SynthetIQ.Function.Domain.Repository.API;

namespace SynthetIQ.Function.Services.Post.API
{
    // this will be picked up by ScanCurrentAssembly in Program.cs
    [RegisterService]
    public sealed class ApiPostSvc
    {
        [InjectService]
        public ApiRepository ApiRepo { get; private set; }

        // Use dependency injection to get a reference to the httpClientFactory and create an
        // instance of our named HttpClient that was defined in Program.cs
        public ApiPostSvc(ApiRepository apiRepository) =>
            ApiRepo = apiRepository
                ?? throw new ArgumentNullException(nameof(ApiRepository));

        // In almost all circumstances each service should have a single ExecuteAsync method
        public async Task<string> ExecuteAsync(string jsonRequest, IPostRequest request, IFunctionResponse response,
            CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            dynamic entity = request.ToEntity(jsonRequest);
            string jsonResponse = await ApiRepo.PostAsync(ct);

            return response.TryDeserialize(jsonResponse) ? jsonResponse :
                throw new FailedRequestException("Failed to deserialize the response from the API");
        }
    }
}