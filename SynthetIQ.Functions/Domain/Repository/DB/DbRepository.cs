using SynthetIQ.Context;
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
        [InjectService]
        public SynthetIQDbContext SynthetIQDbContext { get; private set; };

        [InjectService]
        public AppInsightsLogger Logger { get; private set; }

        public DbRepository(IDbContextFactory<SynthetIQDbContext> dbContextFactory, AppInsightsLogger logger)
        {
            SynthetIQDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            SynthetIQDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Find an instance of an entity by its primary key and type
        /// </summary>
        /// <param name="entityType"> The entity type (table) name </param>
        /// <param name="id">         Row identifier </param>
        /// <param name="ct">         Cancellation token </param>
        /// <returns> </returns>
        public async Task<dynamic> GetEntityAsync(Type entityType, Guid id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            return await SynthetIQDbContext.FindAsync(entityType, id, ct);
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
            _ = await SynthetIQDbContext.Update(entity);
            _ = await SynthetIQDbContext.SaveChangesAsync();

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
              SynthetIQDbContext.BulkInsertAsync(entities, options =>
                {
                    options.InsertIfNotExists = true;
                    options.AutoMapOutputDirection = false; // prevents pkey from being returned
                },
             ct);

            return true;
        }

        /// <summary>
        /// Provides a way to delete an entity from the database based on its type and ID.
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEntityAsync(Type entityType, Guid id, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            var entity = await SynthetIQDbContext.FindAsync(entityType, id);
            if (entity != null)
            {
                SynthetIQDbContext.Remove(entity);
                await SynthetIQDbContext.SaveChangesAsync(ct);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Default Behavior: For simple CRUD operations involving a single DbContext instance and ending with a SaveChanges() call, EF's default transaction handling is sufficient and automatically ensures atomicity.
        /// 
        /// Complex Scenarios: When your operation spans multiple discrete tasks that should either all succeed or all fail together but aren't encapsulated within a single SaveChanges() call, explicitly defining a 
        /// transaction scope becomes necessary. This might include complex business logic that spans multiple database tables or even databases, batch processing, or situations where you're integrating with external systems
        /// or APIs as part of what you consider a single logical operation.
        /// 
        /// Performance Considerations: Explicit transactions can also be used to batch multiple operations together to improve performance.By default, every SaveChanges() call in EF is a transaction, which can be expensive 
        /// if you're making many small changes individually. Wrapping these in a larger transaction can reduce overhead.
        /// </summary>
        /// <param name="operations"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteInTransactionAsync(Func<Task> operations, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            // CreateExecutionStrategy() is useful for handling retries in environments where transient failures are expected. This method becomes especially useful when
            // dealing with cloud databases that might temporarily be unreachable or locked.
            var strategy = SynthetIQDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await SynthetIQDbContext.Database.BeginTransactionAsync(ct))
                {
                    try
                    {
                        await operations();
                        await SynthetIQDbContext.SaveChangesAsync(ct);
                        await transaction.CommitAsync(ct);
                    }
                    catch
                    {
                        // Transaction will automatically be rolled back on dispose if not committed
                        throw;
                    }
                }
            });

            return true;
        }
    }
}