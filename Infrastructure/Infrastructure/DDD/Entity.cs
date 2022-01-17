using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.DDD.Interface;
using Infrastructure.Exceptions;

namespace Infrastructure.DDD
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    public abstract class Entity :DDDBuildingBlock
    {
        public Guid Id { get; private set; }
        
        private List<IDomainEvent> _domainEvents;
  
        private List<IntegrationEvent.IntegrationEvent> _integrationEvents;

        /// <summary>
        /// Domain events occurred.
        /// </summary>
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly()??new ReadOnlyCollection<IDomainEvent>(new List<IDomainEvent>());

        [NotMapped]
        public IReadOnlyCollection<IntegrationEvent.IntegrationEvent> IntegrationEvents => _integrationEvents?.AsReadOnly();
        /// <summary>
        /// Add domain event.
        /// </summary>
        /// <param name="domainEvent"></param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new List<IDomainEvent>();
            this._domainEvents.Add(domainEvent);
        }

        protected void AddIntegrationEvent(IntegrationEvent.IntegrationEvent integrationEvent)
        {
            _integrationEvents ??= new List<IntegrationEvent.IntegrationEvent>();
            this._integrationEvents.Add(integrationEvent);
        }

        /// <summary>
        /// Clear domain events.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}