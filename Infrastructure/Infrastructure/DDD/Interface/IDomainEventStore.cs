using System.Collections.Generic;

namespace Infrastructure.DDD.Interface
{
    public interface IDomainEventStore
    {
        void AddDomainEvent(Entity entity, IDomainEvent domainEvent);
        void Clear();
        IEnumerable<IDomainEvent> GetDomainEvents();
        IEnumerable<IDomainEvent> GetDomainEvents(Entity entity);
    }
}