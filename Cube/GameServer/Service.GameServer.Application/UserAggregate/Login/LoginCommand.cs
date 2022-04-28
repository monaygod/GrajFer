using Infrastructure.Application.Command;

namespace Service.GameServer.Application.UserAggregate.Login
{
    public class LoginCommand : CommandBase<LoginCommandResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}