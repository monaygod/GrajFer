using System;
using Infrastructure.DDD.Interface;

namespace Service.GameServer.Domain.Events;

public class ActiveElementMovedEvent : IDomainEvent
{
    public Guid RoomId { get; private set; }

    public DateTime OccurredOn { get; private set; }

    public ActiveElementMovedEvent(Guid roomId)
    {
        RoomId = roomId;
        OccurredOn = DateTime.Now;
    }
}