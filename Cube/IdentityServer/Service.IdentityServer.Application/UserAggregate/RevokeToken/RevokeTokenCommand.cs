using Hawk.Infrastructure.Main.Application.Command;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.RevokeToken
{
    public class RevokeTokenCommand : CommandBase
    {
        public string RefreshToken { get; set; }
    }
}