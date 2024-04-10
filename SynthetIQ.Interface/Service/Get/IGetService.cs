namespace SynthetIQ.Interface.Service.Get
{
    public interface IGetService
    {
        public Task<IFunctionResponse> ExecuteAsync(IApiGetRequest request, CancellationToken token);
    }
}