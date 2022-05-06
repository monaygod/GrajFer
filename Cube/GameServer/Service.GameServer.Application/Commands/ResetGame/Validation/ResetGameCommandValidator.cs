using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.CreateRoom.Validation
{
    public class ResetGameCommandValidator: CommandValidator<ResetGameCommand>
    {
        public ResetGameCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
        }
    }
}