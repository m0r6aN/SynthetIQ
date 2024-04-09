namespace SynthetIQ.Interface.Repository.API
{
    public interface IApiRepository
    {
        public Task<string> GetAsync(CancellationToken token);

        public Task<string> PostAsync(object content, CancellationToken token);

        public Task<string> PutAsync(object content, CancellationToken token);

        public Task<string> PatchAsync(object content, CancellationToken token);

        public Task<string> DeleteAsync(CancellationToken token);
    }
}