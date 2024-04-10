namespace SynthetIQ.Interface.Value.Request
{
    public interface IApiGetRequest
    {
        Task<Uri> ToUri();

        Task<string> FanOutAndInAsync(CancellationToken token);

        bool CanSerialize();
    }
}