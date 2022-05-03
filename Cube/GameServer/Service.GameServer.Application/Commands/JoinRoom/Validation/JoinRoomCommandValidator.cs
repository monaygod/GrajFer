using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.JoinRoom.Validation
{
    public class JoinRoomCommandValidator: CommandValidator<JoinRoomCommand>
    {
        public JoinRoomCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
            When(x=>x.Password is not null, () => {
                RuleFor(y => y.Password).MinimumLength(6);
            });
        }
    }
}