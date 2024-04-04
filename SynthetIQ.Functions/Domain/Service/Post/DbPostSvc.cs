using SynthetIQ.Function.Domain.Repository.DB;

namespace SynthetIQ.Function.Domain.Service.Post
{
    [RegisterService]
    public sealed class DbPostSvc
    {
        [InjectService]
        public DbRepository DbRepo { get; private set; }

        public DbPostSvc(DbRepository dbRepository) =>
            DbRepo = dbRepository
                ?? throw new ArgumentNullException(nameof(dbRepository));

        public async Task<bool> ExecuteAsync(dynamic data, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();

            //data.Adapt<IEnumerable<AbbyyLoadLocations>>();

            return await DbRepo.BulkInsertAsync(data, ct);
        }
    }
}