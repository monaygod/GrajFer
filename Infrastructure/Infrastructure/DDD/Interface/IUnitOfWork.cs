using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DDD.Interface
{
    public interface IUnitOfWork<TContext> where TContext: DbContext
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        void RegisterAggregateRoot(IAggregateRoot o);
    }
}