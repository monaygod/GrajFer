using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.RefreshAccessToken
{
    public class RefreshAccessTokenCommandResult : CommandResultBase
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}