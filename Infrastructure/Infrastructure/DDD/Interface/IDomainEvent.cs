using System;
using MediatR;

namespace Infrastructure.DDD.Interface
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}