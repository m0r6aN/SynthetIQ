namespace SynthetIQ.Interface.Value.Request
{
    public interface IGetRequest
    {
        Task<Uri> ToUri();

        Task<string> FanOutAndInAsync(CancellationToken token);

        bool CanSerialize();
    }
}