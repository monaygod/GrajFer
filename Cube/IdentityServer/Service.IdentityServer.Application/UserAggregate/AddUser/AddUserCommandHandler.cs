using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using MediatR;
using Service.IdentityServer.Domain.UserAggregate;
using Service.IdentityServer.Repository;

namespace Service.IdentityServer.Application.UserAggregate.AddUser
{
    public class AddUserCommandHandler : ICommandHandlerBase<AddUserCommand>
    {
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork<UserContext> _unitOfWork;
        private readonly UserContext _dbContext;

        public AddUserCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork,
            UserContext dbContext)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            _dbContext.Users.Add(
                new User(
                    command.Id,
                    command.UserName,
                    command.Password,
                    command.Claims
                    )
                );
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}