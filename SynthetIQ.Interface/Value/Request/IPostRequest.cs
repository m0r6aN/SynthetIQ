namespace SynthetIQ.Interface.Value.Request
{
    public interface IPostRequest
    {
        public bool CanDeserialize(string json);

        public dynamic ToEntity(string json);
    }
}