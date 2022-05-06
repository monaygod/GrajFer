using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.JoinRoom
{
    public class JoinRoomCommandHandler : ICommandHandlerBase<JoinRoomCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public JoinRoomCommandHandler(RoomRepository roomRepository,
            PlayerRepository playerRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context)
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
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

            var player = _playerRepository.GetById(userId);
            
            if (player is null)
            {
                throw new BadHttpRequestException("Player not connected to socket!");
            }
            
            if (!room.JoinRoom(player))
            {
                throw new BadHttpRequestException("Player already in room!");
            }
            
            _unitOfWork.RegisterAggregateRoot(player);
            _unitOfWork.RegisterAggregateRoot(room);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}