using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using Infrastructure.Mapping;
using Service.IdentityServer.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Service.IdentityServer.Application.UserAggregate.RefreshAccessToken
{
    public class RefreshAccessTokenCommandHandler : ICommandHandlerBase<RefreshAccessTokenCommand, RefreshAccessTokenCommandResult>
    {
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork<UserContext> _unitOfWork;

        public RefreshAccessTokenCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<RefreshAccessTokenCommandResult> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokenToBytes = Convert.FromBase64String(request.RefreshToken);
            var user = _userRepository.GetByRefreshToken(refreshTokenToBytes);

            if (!user.CheckAccessTokenExpireDate())
            {
                throw new SecurityTokenExpiredException("Token expired!");
            }

            var result = new RefreshAccessTokenCommandResult()
            {
                RefreshToken = user.GenerateRefreshToken(request.RefreshToken),
            };
            result.AccessToken = user.GenerateAccessToken(result.RefreshToken);

            _unitOfWork.RegisterAggregateRoot(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return result;
        }
    }
}