using System;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.JoinRoom
{
    public class JoinRoomCommand : CommandBase
    {
        public Guid RoomId { get; set; }
        public string Password { get; set; }
    }
}