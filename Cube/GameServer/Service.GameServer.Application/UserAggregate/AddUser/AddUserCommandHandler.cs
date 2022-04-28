using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.UserAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.UserAggregate.AddUser
{
    public class AddUserCommandHandler : ICommandHandlerBase<AddUserCommand, AddUserCommandResult>
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
        public async Task<AddUserCommandResult> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            _dbContext.NoTaks.Add(NoTak.Create());
            _dbContext.Users.Add(User.Create());
            _dbContext.SaveChanges();
            var result = new AddUserCommandResult()
            {
            };
            return result;
        }
    }
}