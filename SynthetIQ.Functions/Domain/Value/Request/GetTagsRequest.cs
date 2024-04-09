namespace SynthetIQ.Function.Domain.Value.Request
{
    public sealed class GetTagsRequest : RequestBase, IGetRequest
    {
        public List<string> Hints { get; set; }

        public GetTagsRequest(string hints)
        {
            Hints = hints.Split(',').ToList();
        }

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
            //UriBuilder builder = new UriBuilder(Settings.BaseApimUrl);
            //builder.Port = 443;
            //builder.Scheme = "https";
            //builder.Path = "insights/fuelcosts";
            //builder.Query = $"pickupZip={pickupZip},pickupCity{pickupCity}";
            //return Task.FromResult(builder.Uri);
        }
    }
}