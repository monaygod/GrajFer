using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using Infrastructure.Mapping;
using Service.IdentityServer.Repository;
using Microsoft.AspNetCore.Http;

namespace Service.IdentityServer.Application.UserAggregate.Login
{
    public class LoginCommandHandler : ICommandHandlerBase<LoginCommand, LoginCommandResult>
    {
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork<UserContext> _unitOfWork;

        public LoginCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginCommandResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(command.Email);

            if (!user.ValidatePassword(command.Password))
            {
                throw new BadHttpRequestException("Wrong password!");
            }
            var result = new LoginCommandResult()
            {
                RefreshToken = user.GenerateRefreshToken(),
            };
            result.AccessToken = user.GenerateAccessToken(result.RefreshToken);
            _unitOfWork.RegisterAggregateRoot(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return result;
        }
    }
}