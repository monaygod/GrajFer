using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Command.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Commands.MoveElement
{
    public class MoveElementCommandHandler : ICommandHandlerBase<MoveElementCommand>
    {
        private readonly RoomRepository _roomRepository;
        private readonly PlayerRepository _playerRepository;
        private readonly IUnitOfWork<GameDbContext> _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public MoveElementCommandHandler(RoomRepository roomRepository,
            PlayerRepository playerRepository,
            IUnitOfWork<GameDbContext> unitOfWork,
            IHttpContextAccessor  context)
        {
            _roomRepository = roomRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<Unit> Handle(MoveElementCommand command, CancellationToken cancellationToken)
        {
            if(_context.HttpContext!.Items["User"] == null) throw new AuthenticationException("Authentication error!");

            var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;
            var user = _playerRepository.GetById(userId);
            if (user is null)
            {
                user = new Player(userId);
                //throw new BadHttpRequestException("Player not connected to socket!");
            }
            var room = _roomRepository.GetById(command.RoomId);
            if (!room.IsPlayerInRoom(user))
            {
                throw new BadHttpRequestException("Player not in room!");
            }
            room.MoveActiveElement(command.SourceStaticFieldId, command.DestinationStaticFieldId, command.ActiveElementId);
           
            _unitOfWork.RegisterAggregateRoot(user);
            _unitOfWork.RegisterAggregateRoot(room);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}