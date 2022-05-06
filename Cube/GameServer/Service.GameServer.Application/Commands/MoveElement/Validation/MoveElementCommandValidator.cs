using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameServer.Application.Commands.MoveElement.Validation
{
    public class MoveElementCommandValidator: CommandValidator<MoveElementCommand>
    {
        public MoveElementCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.SourceStaticFieldId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.DestinationStaticFieldId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ActiveElementId)
                .NotEmpty()
                .NotNull();
        }
    }
}