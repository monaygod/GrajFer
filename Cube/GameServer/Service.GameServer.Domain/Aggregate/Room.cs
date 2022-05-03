using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.Events;
using Service.GameServer.Domain.ValueObject;

namespace Service.GameServer.Domain.UserAggregate
{
    public class Room : Entity, IAggregateRoot
    {
        
        private string _roomName;
        private Password _password;
        private Guid _gameId;
        private string _description;
        private int _maxPlayers;
        private Player _host;
        private List<Player> _players;
        private Room() { }
        public string RoomName => _roomName;
        [JsonIgnore]
        public Password Password => _password;
        public Guid GameId => _gameId;
        public string Description => _description;
        public int MaxPlayers => _maxPlayers;
        public Player Host => _host;
        public IReadOnlyCollection<Player> Players => _players;
        
        public Room(Guid Id, string roomName, string password, Guid gameId, string description, int maxPlayers, Guid host)
        {
            _roomName = roomName;
            _password = new Password(password);
            _gameId = gameId;
            _description = description;
            _maxPlayers = maxPlayers;
            _host = new Player(host);
            _players = new List<Player>(){ new (host)};
            AddDomainEvent(new NewRoomEvent(Id));
        }
        
        public bool ValidatePassword(string pass)
        {
            return Password.ValidatePassword(pass);
        }

        public bool JoinRoom(Guid playerId)
        {
            if (Players.Any(x => x.PlayerId == playerId))
                return false;
            _players.Add(new Player(playerId));
            AddDomainEvent(new PlayerJoinedRoomEvent(Id, playerId));
            return true;
        }
        
        public bool LeaveRoom(Guid playerId)
        {
            var player = Players.FirstOrDefault(x => x.PlayerId == playerId);
            if (player is null)
                return false;
            _players.Remove(player);
            AddDomainEvent(new PlayerLeaveRoomEvent(Id, playerId));
            return true;
        }

    }
}