using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Hawk.Infrastructure.Main.Application.Command.Interface;
using Hawk.Infrastructure.Main.Auth.Model;
using Hawk.Infrastructure.Main.DDD.Interface;
using Hawk.Infrastructure.Main.Mapping;
using Hawk.Service.IdentityServer.Domain.UserAggregate;
using Infrastructure.Application.Command.Interface;
using Infrastructure.DDD.Interface;
using Infrastructure.Mapping;
using Service.IdentityServer.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Service.IdentityServer.Application.UserAggregate.RevokeToken
{
    public class RevokeTokenCommandHandler : ICommandHandlerBase<RevokeTokenCommand>
    {
        private readonly UserRepository _userRepository;
        private readonly IUnitOfWork<UserContext> _unitOfWork;
        private readonly IEntityMapper<User, Repository.Model.User> _mapper;
        private readonly IHttpContextAccessor  _context;

        public RevokeTokenCommandHandler(UserRepository userRepository,
            IUnitOfWork<UserContext> unitOfWork,
            IEntityMapper<User, Repository.Model.User> mapper, IHttpContextAccessor  context)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {

            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userid = Convert.ToInt32(((AccessToken) _context.HttpContext.Items["User"])?.UserId);
            var user = _userRepository.GetById(userid);

            user.RevokeRefreshToken(request.RefreshToken);
            _unitOfWork.RegisterAggregateRoot(user);
            _mapper.MapToDbObject(user);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}