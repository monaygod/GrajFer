using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.Login.Validation
{
    public class LoginCommandValidator: CommandValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();
                //.WithMessage("Wrong username format!");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
               // .WithMessage("Wrong password format!");
        }
    }
}