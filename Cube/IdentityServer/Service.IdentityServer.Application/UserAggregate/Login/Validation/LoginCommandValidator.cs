using FluentValidation;
using Hawk.Infrastructure.Main.Application.Command;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.Login.Validation
{
    public class LoginCommandValidator: CommandValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
                //.WithMessage("Wrong email format!");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
               // .WithMessage("Wrong password format!");
        }
    }
}