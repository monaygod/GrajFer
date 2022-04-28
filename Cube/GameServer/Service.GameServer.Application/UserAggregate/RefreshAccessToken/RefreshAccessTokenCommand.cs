using Infrastructure.Application.Command;

namespace Service.GameServer.Application.UserAggregate.RefreshAccessToken
{
    public class RefreshAccessTokenCommand : CommandBase<RefreshAccessTokenCommandResult>
    {
        public string RefreshToken { get; set; }  
    }
}