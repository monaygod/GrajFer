﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.DDD.Interface;
using Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace Infrastructure.DDD
{
    public abstract class Entity : DDDBuildingBlock
    {
        public Guid Id;
        private List<IDomainEvent> _domainEvents;
        private List<IntegrationEvent.IntegrationEvent> _integrationEvents;
        
        [NotMapped]
        [JsonIgnore]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly()??new ReadOnlyCollection<IDomainEvent>(new List<IDomainEvent>());

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyCollection<IntegrationEvent.IntegrationEvent> IntegrationEvents => _integrationEvents?.AsReadOnly();
        
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
