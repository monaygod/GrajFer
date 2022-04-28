using Infrastructure.Application.Command;

namespace Service.GameServer.Application.UserAggregate.RevokeToken.Validation
{
    public class RevokeTokenCommandValidator : CommandValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {

        }
    }
}