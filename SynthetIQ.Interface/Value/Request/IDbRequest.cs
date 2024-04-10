using System.Linq.Expressions;

namespace SynthetIQ.Interface.Value.Request
{
    public interface IDbRequest
    {
        Expression<Func<TEntity, bool>> ToDelegate<TEntity>();
    }
}