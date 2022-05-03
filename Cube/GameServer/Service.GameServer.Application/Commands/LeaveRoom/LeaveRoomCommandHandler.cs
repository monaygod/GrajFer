using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.LeaveRoom
{
    public class LeaveRoomCommandHandler : ICommandHandlerBase<LeaveRoomCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public LeaveRoomCommandHandler(RoomRepository roomRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Unit> Handle(LeaveRoomCommand command, CancellationToken cancellationToken)
        {
            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;

            var room = _roomRepository.GetById(command.RoomId);
            
            if (!room.LeaveRoom(userId))
            {
                throw new BadHttpRequestException("Player not in room!");
            }
            
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}