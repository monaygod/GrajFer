using System.Collections.Generic;
using System.Linq;
using Infrastructure.DDD.Interface;

namespace Infrastructure.DDD
{
    public class DomainEventStore : IDomainEventStore
    {
        private Dictionary<Entity, List<IDomainEvent>> _domainEvents;

        public void AddDomainEvent(Entity entity, IDomainEvent domainEvent)
        {
            if (_domainEvents.TryGetValue(entity, out var events))
            {
                events.Add(domainEvent);
            }
            else
            {
                _domainEvents.Add(entity, new List<IDomainEvent>() {domainEvent});
            }
        }

        public void Clear()
        {
            _domainEvents.Clear();
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.SelectMany(x => x.Value);
        }

        public IEnumerable<IDomainEvent> GetDomainEvents(Entity entity)
        {
            if (_domainEvents.TryGetValue(entity, out var events)
            )
            {
                return events;
            }

            return null;
        }
    }
}