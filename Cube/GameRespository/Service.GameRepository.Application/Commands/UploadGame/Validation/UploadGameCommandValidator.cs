using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.GameRepository.Application.Commands.UploadGame.Validation
{
    public class UploadGameCommandValidator: CommandValidator<UploadGameCommand>
    {
        public UploadGameCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.GameName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.File)
                .NotNull();
        }
    }
}