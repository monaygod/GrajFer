using FluentValidation;
using Infrastructure.Application.Command;
using Service.GameServer.Domain.RoomAggregate;

namespace Service.GameServer.Application.Commands.StartGame.Validation
{
    public class StartGameCommandValidator: CommandValidator<StartGameCommand>
    {
        public StartGameCommandValidator()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.InitialGameState)
                .NotNull();
            RuleForEach(x => x.InitialGameState)
                .SetValidator(new StaticFieldValidator());
        }
    }
    public class StaticFieldValidator : AbstractValidator<StaticField> 
    {
        public StaticFieldValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.ActiveElements)
                .NotNull();
            RuleForEach(x => x.ActiveElements)
                .SetValidator(new ActiveElementValidator());
        }
    }
    public class ActiveElementValidator : AbstractValidator<ActiveElement> 
    {
        public ActiveElementValidator() 
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}