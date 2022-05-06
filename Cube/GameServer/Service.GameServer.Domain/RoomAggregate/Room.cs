using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Infrastructure.DDD;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.Enums;
using Service.GameServer.Domain.Events;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Domain.ValueObject;

namespace Service.GameServer.Domain.RoomAggregate
{
    public class Room : Entity, IAggregateRoot
    {
        
        private string _roomName;
        private Password _password;
        private Game _game;
        private Guid _gameId;
        private string _description;
        private int _maxPlayers;
        private Room() { }
        public string RoomName => _roomName;
        [JsonIgnore]
        public Password Password => _password;

        public Game Game => _game;
        public Guid GameId => _gameId;
        public string Description => _description;
        public int MaxPlayers => _maxPlayers;
        public Guid Host;
        [JsonIgnore]
        public ICollection<Player> Players { get; }
        
        public Room(Guid Id, string roomName, string password, Guid gameId, string description, int maxPlayers, Player host)
        {
            _roomName = roomName;
            _password = new Password(password);
            _gameId = gameId;
            _description = description;
            _maxPlayers = maxPlayers;
            Host = host.PlayerId;
            Players = new List<Player>() { host };
            AddDomainEvent(new NewRoomEvent(Id));
        }
        
        public bool ValidatePassword(string pass)
        {
            return Password.ValidatePassword(pass);
        }

        public bool JoinRoom(Player playerId)
        {
            if (Players.Any(x => x.Id == playerId.Id))
                return false;
            Players.Add(playerId);
            AddDomainEvent(new PlayerJoinedRoomEvent(Id, playerId));
            return true;
        }
        
        public bool LeaveRoom(Player player)
        {
            if (Players.Count == 1)
                return false;
            Players.Remove(player);
            AddDomainEvent(new PlayerLeaveRoomEvent(Id, player));
            if (IsPlayerRoomOwner(player))
            {
                var nextPlayer = Players.First();
                Host = nextPlayer.PlayerId;
            }
            return true;
        }

        public bool IsPlayerInRoom(Player player)
        {
            return Players.Any(x => x.PlayerId == player.PlayerId);
        }

        public bool IsPlayerRoomOwner(Player player)
        {
            return player.PlayerId == Host;
        }

        public void StartGame(ICollection<StaticField> staticFields)
        {
            Game.StartGame(staticFields);
        }

        public void ResetGame()
        {
            Game.ResetGame();
        }

        public void MoveActiveElement(
            Guid sourceFieldId,
            Guid destinationFieldId,
            Guid elementId
        )
        {
            Game.MoveActiveElement(sourceFieldId,destinationFieldId,elementId);
            AddDomainEvent(new ActiveElementMovedEvent(Id));
        }

        public void UsePredefinedFunction(Guid fieldId, Guid elementId, PredefinedFunction predefinedFunction)
        {
            Game.UsePredefinedFunction(fieldId,elementId, predefinedFunction);
            AddDomainEvent(new ActiveElementMovedEvent(Id));
        }
    }
}