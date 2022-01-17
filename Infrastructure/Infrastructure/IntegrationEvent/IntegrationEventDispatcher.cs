using System.Linq;
using Infrastructure.DDD;
using Infrastructure.IntegrationEvent.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationEvent
{
    public class IntegrationEventDispatcher<T> : IIntegrationEventDispatcher<T> where T : DbContext
    {
        private readonly IEventBus _eventBus;
        private readonly T _context;


        public IntegrationEventDispatcher(IEventBus eventBus, T context)
        {
            _eventBus = eventBus;
            _context = context;
        }

        public void DispatchIntegrationEvent()
        {
            var domainEntities = this._context.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.IntegrationEvents != null && x.Entity.IntegrationEvents.Any())
                .ToList();

            var integrationEvents = domainEntities.SelectMany(x => x.Entity.IntegrationEvents)
                .ToList();
            foreach (var integrationEvent in integrationEvents)
            {
                _eventBus.Publish(integrationEvent);
            }
        }
    }
}