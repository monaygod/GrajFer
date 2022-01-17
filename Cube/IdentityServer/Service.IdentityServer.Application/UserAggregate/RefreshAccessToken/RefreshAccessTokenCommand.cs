using Hawk.Infrastructure.Main.Application.Command;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.RefreshAccessToken
{
    public class RefreshAccessTokenCommand : CommandBase<RefreshAccessTokenCommandResult>
    {
        public string RefreshToken { get; set; }  
    }
}