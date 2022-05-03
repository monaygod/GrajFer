using System;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.CreateRoom
{
    public class CreateRoomCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public Guid GameId { get; set; }
        public int MaxPlayers { get; set; }
        public string Password { get; set; }
    }
}