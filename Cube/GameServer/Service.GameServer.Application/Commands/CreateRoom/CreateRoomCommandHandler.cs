using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : ICommandHandlerBase<CreateRoomCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public CreateRoomCommandHandler(RoomRepository roomRepository,
            PlayerRepository playerRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context)
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Unit> Handle(CreateRoomCommand command, CancellationToken cancellationToken)
        {
            if(_context.HttpContext.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;
            var user = _playerRepository.GetById(userId);
            if (user is null)
            {
                throw new BadHttpRequestException("Player not connected to socket!");
            }
            var newGame = new Room(command.Id,
                command.RoomName,
                command.Password,
                command.GameId,
                command.Description,
                command.MaxPlayers,
                user);
            
            _roomRepository.Add(newGame);
            
            _unitOfWork.RegisterAggregateRoot(user);
            _unitOfWork.RegisterAggregateRoot(newGame);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}