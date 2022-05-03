using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using Service.IdentityServer.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Service.IdentityServer.Application.UserAggregate.RevokeToken
{
    public class RevokeTokenCommandHandler : ICommandHandlerBase<RevokeTokenCommand>
    {
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork<UserContext> _unitOfWork;
        private readonly IHttpContextAccessor  _context;

        public RevokeTokenCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork, IHttpContextAccessor  context)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userid = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;
            var user = await _userRepository.GetByIdAsync(userid);

            user.RevokeRefreshToken(request.RefreshToken);
            _unitOfWork.RegisterAggregateRoot(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}