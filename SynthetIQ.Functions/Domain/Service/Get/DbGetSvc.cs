using SynthetIQ.Function.Domain.Repository.DB;
using SynthetIQ.Function.Domain.Value.Request;

namespace SynthetIQ.Function.Services.Get.API
{
    // this will be picked up by ScanCurrentAssembly in Program.cs
    [RegisterService]
    public sealed class DbGetSvc
    {
        [InjectService]
        public DbRepository DbRepo { get; private set; }

        // use dependency injection to get a reference to the httpClientFactory and create an
        // instance of our named HttpClient that was defined in Program.cs
        public DbGetSvc(DbRepository dbRepo) =>
            DbRepo = dbRepo ?? throw new ArgumentNullException(nameof(dbRepo));

        // in almost all circumstances each service should have a single ExecuteAsync method
        public async Task<string> ExecuteAsync(IDbRequest request, CancellationToken ct, IFunctionResponse response)
        {
            ct.ThrowIfCancellationRequested();

            var lambda = request.ToDelegate<TagRequest>();

            // get the response back as json from the repository
            var result = await DbRepo.FindEntitiesAsync(lambda, ct);
            var json = JsonConvert.SerializeObject(result);

            // validate that the returned json can be deserialized into the response object
            return response.TryDeserialize(json) ? json :
                throw new FailedRequestException("Failed to deserialize the response from DbRepository");
        }
    }
}