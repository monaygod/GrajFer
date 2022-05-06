using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Application.Query.Interface;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Queries.GetGameState;

public class GetRoomListQueryHandler : IQueryHandler<GetGameStateQuery, GetGameStateQueryResult>
{
    private readonly RoomRepository _roomRepository;
    private readonly PlayerRepository _playerRepository;
    private readonly IUnitOfWork<GameDbContext> _unitOfWork;
    private readonly IHttpContextAccessor _context;

    public GetRoomListQueryHandler(RoomRepository roomRepository,
        PlayerRepository playerRepository,
        IUnitOfWork<GameDbContext> unitOfWork,
        IHttpContextAccessor  context)
    {
        _roomRepository = roomRepository;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _context = context;
    }
    
    public async Task<GetGameStateQueryResult> Handle(GetGameStateQuery query, CancellationToken cancellationToken)
    {
        if(_context.HttpContext!.Items["User"] == null) throw new AuthenticationException("Authentication error!");

        var userId = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;
        var user = await _playerRepository.GetByIdAsync(userId);
        if (user is null)
        {
            throw new BadHttpRequestException("Player not connected to socket!");
        }
        var room = await _roomRepository.GetByIdAsync(query.RoomId);
        if (!room.IsPlayerInRoom(user))
        {
            throw new BadHttpRequestException("Player not in room!");
        }
        
        return new GetGameStateQueryResult()
        {
            GameState = room.Game
        };
    }
}