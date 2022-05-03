using System;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.LeaveRoom
{
    public class LeaveRoomCommand : CommandBase
    {
        public Guid RoomId { get; set; }
    }
}