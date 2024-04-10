using SynthetIQ.DbContext.Context;
using SynthetIQ.Utility.Logging;

using System.Linq.Expressions;

namespace SynthetIQ.Function.Domain.Repository.DB
{
    /// <summary>
    /// if you were making Tableau database calls they would happen in this class
    /// </summary>
    [RegisterService]
    public sealed class DbRepository
    {
        private readonly SynthetIQContext _dbContext;

        [InjectService]
        public AppInsightsLogger Logger { get; private set; }

        public DbRepository(IDbContextFactory<SynthetIQContext> dbContextFactory, AppInsightsLogger logger)
        {
            _dbContext = dbContextFactory.CreateDbContext() ??
                throw new ArgumentNullException(nameof(dbContextFactory));

            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            Logger = logger;
        }

        /// <summary>
        /// Find an instance of an entity by its primary key and type
        /// </summary>
        /// <param name="entityType"> </param>
        /// <param name="id">         </param>
        /// <param name="ct">         </param>
        /// <returns> </returns>
        public async Task<dynamic> GetEntityAsync(Type entityType, Guid id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await _dbContext.FindAsync(entityType, id, ct);
        }

        /// <summary>
        /// Finds entities based on a given search criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The search criteria as an expression.</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns>A list of entities matching the search criteria.</returns>
        /// <example>
        ///  var invoicesOver100 = await repository.FindEntitiesAsync<Invoice>(invoice => invoice.Total > 100, cancellationToken);
        ///</example>
        public async Task<List<TEntity>> FindEntitiesAsync<TEntity>(Expression<Func<TEntity, bool>> criteria, CancellationToken ct) where TEntity : class
        {
            ct.ThrowIfCancellationRequested();

            return await _dbContext.Set<TEntity>()
                                   .Where(criteria)
                                   .ToListAsync(ct);
        }

        /// <summary>
        /// This is a simple insert or update example
        /// </summary>
        /// <param name="invoice"> </param>
        /// <returns> </returns>
        public async Task<bool> UpsertEntityAsync(dynamic entity, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // using .Update causes Entity Framework to add it if it doesn't exist or update if it
            // does call await immediately to avoid threading issues
            _ = await _dbContext.Update(entity);
            _ = await _dbContext.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Super fast bulk insert
        /// </summary>
        /// <param name="entities"> </param>
        /// <param name="ct">       </param>
        /// <returns> </returns>
        public async Task<bool> BulkInsertAsync(IEnumerable<dynamic> entities, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            await
              _dbContext.BulkInsertAsync(entities, options =>
              {
                  options.InsertIfNotExists = true;
                  options.AutoMapOutputDirection = false; // prevents pkey from being returned
              },
             ct);

            return true;
        }
    }
}