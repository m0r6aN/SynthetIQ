namespace SynthetIQ.Function.Domain.Value.Request
{
    public sealed class GetRateRequest : RequestBase, IGetRequest
    {
        public bool CanSerialize()
        {
            return this.ToJson().Length > 0;
        }

        public Task<string> FanOutAndInAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> ToUri()
        {
            throw new NotImplementedException();
        }
    }
}