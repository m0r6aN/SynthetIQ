namespace SynthetIQ.Functions.Domain.Value.Agents
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Prompt { get; set; }
        public Models.Model Model { get; set; }
        public List<Capability> Capabilities { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}