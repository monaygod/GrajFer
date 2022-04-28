using Infrastructure.Application.Command;

namespace Service.GameServer.Application.UserAggregate.RevokeToken
{
    public class RevokeTokenCommand : CommandBase
    {
        public string RefreshToken { get; set; }
    }
}