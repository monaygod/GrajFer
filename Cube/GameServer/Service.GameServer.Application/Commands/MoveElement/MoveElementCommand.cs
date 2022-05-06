using System;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.MoveElement
{
    public class MoveElementCommand : CommandBase
    {
        public Guid RoomId { get; set; }
        public Guid SourceStaticFieldId { get; set; }
        public Guid DestinationStaticFieldId { get; set; }
        public Guid ActiveElementId { get; set; }
    }
}