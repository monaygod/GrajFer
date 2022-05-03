using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Domain.UserAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.JoinRoom
{
    public class JoinRoomCommandHandler : ICommandHandlerBase<JoinRoomCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public JoinRoomCommandHandler(RoomRepository roomRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Unit> Handle(JoinRoomCommand command, CancellationToken cancellationToken)
        {
            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;

            var room = _roomRepository.GetById(command.RoomId);
            
            if (!room.ValidatePassword(command.Password))
            {
                throw new BadHttpRequestException("Wrong password!");
            }
            if (!room.JoinRoom(userId))
            {
                throw new BadHttpRequestException("Player already in room!");
            }
            
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}