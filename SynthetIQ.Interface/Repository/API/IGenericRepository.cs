namespace SynthetIQ.Interface.Repository.API
{
    public interface IApiRepository
    {
        public Task<string> GetAsync(CancellationToken token);

        public Task<string> PostAsync(CancellationToken token);
    }
}