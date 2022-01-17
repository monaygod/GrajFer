using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using MediatR;

namespace Infrastructure.DDD
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;

        public DomainEventsDispatcher(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public async Task DispatchEventsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task DispatchEventsAsync(ICollection<Entity> entities)
        {
            var domainEvents = GetDomainEvents(entities);

            var tasks = domainEvents
                .Select(async (domainEvent) => { await _mediator.Publish(domainEvent); });

            await Task.WhenAll(tasks);
        }

        private static IEnumerable<IDomainEvent> GetDomainEvents(IEnumerable<Entity> entities)
        {
            List<IDomainEvent> resultDomainEvents = new List<IDomainEvent>();
            foreach (var entity in entities)
            {
                resultDomainEvents.AddRange(entity.DomainEvents);
                
                var type = entity.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties)
                {
                    var propertyType = property.PropertyType;

                    if (propertyType.BaseType == typeof(Entity))
                    {
                        var value = (Entity) property.GetValue(entity);
                        resultDomainEvents.AddRange(GetDomainEvents(new List<Entity>() {value}));
                        continue;
                    }

                    var genericTypes = propertyType.GenericTypeArguments
                        .Select(x => x.BaseType)
                        .ToArray();
                    
                    var isEnumerable = propertyType
                        .GetInterfaces()
                        .Any(x => x.UnderlyingSystemType == typeof(IEnumerable));
                    
                    if (isEnumerable && genericTypes.Any(x => x != null && x == typeof(Entity)))
                    {
                        var val = property.GetValue(entity, null);
                        if (val != null)
                        {
                            resultDomainEvents.AddRange(GetDomainEvents(((IEnumerable) val).Cast<Entity>()));
                        }
                    }
                }
            }

            return resultDomainEvents;
        }
    }
}