using System;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.CreateRoom
{
    public class ResetGameCommand : CommandBase
    {
        public Guid RoomId { get; set; }
    }
}