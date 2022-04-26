using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.RevokeToken.Validation
{
    public class RevokeTokenCommandValidator : CommandValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {

        }
    }
}