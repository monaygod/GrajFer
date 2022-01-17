using Hawk.Infrastructure.Main.Application.Command;
using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.Login
{
    public class LoginCommand : CommandBase<LoginCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}