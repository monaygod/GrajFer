using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.LeaveRoom.Validation
{
    public class LeaveRoomCommandValidator: CommandValidator<LeaveRoomCommand>
    {
        public LeaveRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
        }
    }
}