using Infrastructure.Application.Command;

namespace Service.IdentityServer.Application.UserAggregate.AddUser.Validation
{
    public class AddUserCommandValidator: CommandValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
        }
    }
}