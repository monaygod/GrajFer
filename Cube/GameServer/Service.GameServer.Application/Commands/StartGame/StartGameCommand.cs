using System;
using System.Collections.Generic;
using Infrastructure.Application.Command;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Application.Commands.StartGame
{
    public class StartGameCommand : CommandBase
    {
        public Guid RoomId { get; set; }
        public List<StaticField> InitialGameState { get; set; }
    }
}