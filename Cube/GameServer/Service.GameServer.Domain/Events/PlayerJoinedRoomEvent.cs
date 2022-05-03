using System;
using Infrastructure.DDD.Interface;

namespace Service.GameServer.Domain.Events;

public class PlayerJoinedRoomEvent : IDomainEvent
{
    public Guid RoomId { get; private set; }
    public Guid PlayerId { get; private set; }

    public DateTime OccurredOn { get; private set; }

    public PlayerJoinedRoomEvent(Guid roomId, Guid playerId)
    {
        RoomId = roomId;
        PlayerId = playerId;
        OccurredOn = DateTime.Now;
    }
}