using System;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.PlayerAggregate;

namespace Service.GameServer.Domain.Events;

public class PlayerJoinedRoomEvent : IDomainEvent
{
    public Guid RoomId { get; private set; }
    public Player PlayerId { get; private set; }

    public DateTime OccurredOn { get; private set; }

    public PlayerJoinedRoomEvent(Guid roomId, Player player)
    {
        RoomId = roomId;
        PlayerId = player;
        OccurredOn = DateTime.Now;
    }
}