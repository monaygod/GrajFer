using System;
using Infrastructure.Application.Command;
using Service.GameServer.Domain.Enums;

namespace Service.GameServer.Application.Commands.UsePredefinedFunction
{
    public class UsePredefinedFunctionCommand : CommandBase
    {
        public Guid RoomId { get; set; }
        public Guid StaticFieldId { get; set; }
        public Guid ActiveElementId { get; set; }
        public PredefinedFunction PredefinedFunction { get; set; }
    }
}