using FluentValidation;
using Infrastructure.Application.Command;
using Service.GameServer.Application.Commands.CreateRoom;

namespace Service.GameServer.Application.Commands.UsePredefinedFunction.Validation
{
    public class UsePredefinedFunctionCommandValidator: CommandValidator<UsePredefinedFunctionCommand>
    {
        public UsePredefinedFunctionCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.PredefinedFunction)
                .NotEmpty()
                .NotNull();
        }
    }
}