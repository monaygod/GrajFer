using System;

namespace Service.GameServer.Domain.ValueObject;

public class Player : Infrastructure.DDD.ValueObject
{
    public Guid PlayerId { get; set; }
    private Player(){}

    public Player(Guid playerId)
    {
        PlayerId = playerId;
    }
}