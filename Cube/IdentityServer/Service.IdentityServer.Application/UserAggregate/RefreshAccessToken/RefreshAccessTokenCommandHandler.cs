using System;
using System.Threading;
using System.Threading.Tasks;
using Hawk.Infrastructure.Main.Application.Command.Interface;
using Hawk.Infrastructure.Main.DDD.Interface;
using Hawk.Infrastructure.Main.Mapping;
using Hawk.Service.IdentityServer.Domain.UserAggregate;
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
        private readonly IEntityMapper<User, Repository.Model.User> _mapper;

        public RefreshAccessTokenCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork,
            IEntityMapper<User, Repository.Model.User> mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            _mapper.MapToDbObject(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return result;
        }
    }
}