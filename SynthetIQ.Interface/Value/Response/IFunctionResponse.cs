namespace SynthetIQ.Interface.Value.Response
{
    public interface IFunctionResponse
    {
        public bool TryDeserialize(string json);
    }
}