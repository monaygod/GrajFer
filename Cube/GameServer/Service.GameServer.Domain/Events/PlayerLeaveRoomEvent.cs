using System;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.PlayerAggregate;

namespace Service.GameServer.Domain.Events;

public class PlayerLeaveRoomEvent : IDomainEvent
{
    public Guid RoomId { get; private set; }

    public Player PlayerId { get; private set; }
    public DateTime OccurredOn { get; private set; }

    public PlayerLeaveRoomEvent(Guid roomId, Player playerId)
    {
        RoomId = roomId;
        PlayerId = playerId;
        OccurredOn = DateTime.Now;
    }
}