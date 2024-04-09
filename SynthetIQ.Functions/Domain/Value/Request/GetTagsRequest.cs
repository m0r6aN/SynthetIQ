namespace SynthetIQ.Function.Domain.Value.Request
{
    public sealed class TagRequest
    {
        public List<string> Hints { get; set; }
        public int AgentId { get; set; }
        public int AssistantId { get; set; }
        public int ChatId { get; set; }
        public int CapabilityId { get; set; }
        public int LLMID { get; set; }
        public int UserId { get; set; }

        public TagRequest(int EntityId, EntityType entityType, string search)
        {
            switch (entityType)
            {
                case EntityType.Agent:
                    AgentId = EntityId;
                    break;

                case EntityType.Assistant:
                    AssistantId = EntityId;
                    break;

                case EntityType.LLM:
                    LLMID = EntityId;
                    break;

                case EntityType.User:
                    UserId = EntityId;
                    break;

                case EntityType.Chat:
                    ChatId = EntityId;
                    break;

                case EntityType.Capability:
                    CapabilityId = EntityId;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }
        }

        public Expression<Func<TEntity, bool>> ToDelegate<TEntity>()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "entity");

            // Assuming each entity type has an ID property that matches the name of your TagRequest
            // properties. This dictionary maps the entity type names to the corresponding ID values
            // in TagRequest.
            var entityToIdMapping = new Dictionary<string, int>
            {
                {"Agent", AgentId},
                {"Assistant", AssistantId},
                {"LLM", LLMID},
                {"User", UserId},
                {"Chat", ChatId},
                {"Capability", CapabilityId}
            };

            Expression predicateBody = null;
            foreach (var mapping in entityToIdMapping)
            {
                if (mapping.Value > 0) // Assuming a non-zero value indicates this is the target entity
                {
                    var idProperty = typeof(TEntity).GetProperty($"{mapping.Key}Id");
                    if (idProperty != null) // Ensuring TEntity has the corresponding Id property
                    {
                        var idValue = Expression.Constant(mapping.Value);
                        var idPropertyAccess = Expression.MakeMemberAccess(parameter, idProperty);
                        var equals = Expression.Equal(idPropertyAccess, idValue);
                        predicateBody = equals;
                        break; // Assuming only one ID field is set per TagRequest instance
                    }
                }
            }

            if (predicateBody == null)
            {
                throw new InvalidOperationException("Unable to construct a predicate from the given TagRequest.");
            }

            return Expression.Lambda<Func<TEntity, bool>>(predicateBody, parameter);
        }
    }
}