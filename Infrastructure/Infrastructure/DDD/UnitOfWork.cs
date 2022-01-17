using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Infrastructure.IntegrationEvent.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DDD
{
    public class UnitOfWork<TContext>: IUnitOfWork<TContext> where TContext: DbContext
    {
        private readonly TContext _dbContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly IIntegrationEventDispatcher<TContext> _integrationEventDispatcher;
        private readonly ICollection<Entity> _domainEventSource;

        public UnitOfWork(TContext dbContext,IDomainEventsDispatcher domainEventsDispatcher, IIntegrationEventDispatcher<TContext> integrationEventDispatcher)
        {
            this._dbContext = dbContext;
             this._domainEventsDispatcher = domainEventsDispatcher;
             _integrationEventDispatcher = integrationEventDispatcher;
             _domainEventSource = new List<Entity>();
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync(_domainEventSource);
            var result = await this._dbContext.SaveChangesAsync(cancellationToken);
            _integrationEventDispatcher.DispatchIntegrationEvent();
            return result;
        }

        public void RegisterAggregateRoot(IAggregateRoot o)
        {
            _domainEventSource.Add((Entity)o);
        }
    }
}