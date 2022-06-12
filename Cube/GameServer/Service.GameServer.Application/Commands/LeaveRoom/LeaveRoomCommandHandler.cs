using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Application.Hubs;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.LeaveRoom
{
    public class LeaveRoomCommandHandler : ICommandHandlerBase<LeaveRoomCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;
        private readonly GameDbContext _dbContext;
        private readonly GameHub _gameHub;

        public LeaveRoomCommandHandler(RoomRepository roomRepository,
            PlayerRepository playerRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context,
            GameDbContext dbContext,
            GameHub gameHub)
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _context = context;
            _dbContext = dbContext;
            _gameHub = gameHub;
        }
        public async Task<Unit> Handle(LeaveRoomCommand command, CancellationToken cancellationToken)
        {
            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;

            var user = await _playerRepository.GetByIdAsync(userId);
            if (user is null)
            {
                user = new Player(userId);
                //throw new BadHttpRequestException("Player not connected to socket!");
            }
            
            var room = await _roomRepository.GetByIdAsync(command.RoomId);
            Task task = null;
            if (!room.IsPlayerInRoom(user))
            {
                throw new BadHttpRequestException("Player not in room!");
            }
            if (!room.LeaveRoom(user))
            {
                _dbContext.Rooms.Remove(room);
                task = _gameHub.SendDeletedRoomInfo(room.Id);
            }
            
            _unitOfWork.RegisterAggregateRoot(user);
            _unitOfWork.RegisterAggregateRoot(room);
            await _unitOfWork.CommitAsync(cancellationToken);
            if(task is not null)
                await task;
            return Unit.Value;
        }
    }
}