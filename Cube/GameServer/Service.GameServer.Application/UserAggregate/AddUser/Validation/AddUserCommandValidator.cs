using Infrastructure.Application.Command;

namespace Service.GameServer.Application.UserAggregate.AddUser.Validation
{
    public class AddUserCommandValidator: CommandValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
        }
    }
}