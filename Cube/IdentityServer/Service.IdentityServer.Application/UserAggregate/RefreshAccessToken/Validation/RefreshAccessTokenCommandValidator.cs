using FluentValidation;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.RefreshAccessToken.Validation
{
    public class RefreshAccessTokenCommandValidator : CommandValidator<RefreshAccessTokenCommand>
    {
        public RefreshAccessTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty()
                .Length(88)
                .WithMessage("Wrong refresh token format!");
        }
    }
}