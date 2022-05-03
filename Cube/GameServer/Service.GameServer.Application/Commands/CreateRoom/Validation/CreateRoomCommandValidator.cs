using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.CreateRoom.Validation
{
    public class CreateRoomCommandValidator: CommandValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.RoomName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Description)
                .NotNull();
            RuleFor(x => x.GameId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.MaxPlayers)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();
            When(x=>x.Password is not null, () => {
                RuleFor(y => y.Password).MinimumLength(6);
            });
        }
    }
}