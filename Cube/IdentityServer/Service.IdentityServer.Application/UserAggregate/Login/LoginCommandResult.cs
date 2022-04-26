
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.Login
{
    public class LoginCommandResult : CommandResultBase
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}