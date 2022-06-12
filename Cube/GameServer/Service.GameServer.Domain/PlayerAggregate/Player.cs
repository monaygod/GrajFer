using System;
using System.Collections.Generic;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Domain.PlayerAggregate;

public class Player : Entity, IAggregateRoot
{
    private Guid _playerId;
    private string _signalrConnectionId;

    public Guid PlayerId => _playerId;
    public string SignalrConnectionId => _signalrConnectionId;
    public ICollection<Room> Rooms { get; set; }
    private Player(){}

    public Player(Guid playerId, string signalrConnectionId)
    {
        _playerId = playerId;
        _signalrConnectionId = signalrConnectionId;
    }

    public Player(Guid playerId)
    {
        _playerId = playerId;
        _signalrConnectionId = String.Empty;
    }

    public void SetConnectionId(string signalrConnectionId)
    {
        _signalrConnectionId = signalrConnectionId;
    }
}