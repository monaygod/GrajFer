using System;
using System.Security.Authentication;
using System.Threading.Tasks;
using Infrastructure.Auth.JwtUtils;
using Infrastructure.Auth.Model;
using Infrastructure.DDD.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Service.GameServer.Domain.PlayerAggregate;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Domain.ValueObject;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.Hubs;

public class GameHub : Hub
{
    private readonly GameDbContext _dbContext;
    private readonly IHttpContextAccessor _context;
    private readonly PlayerRepository _playerRepository;
    private readonly IUnitOfWork<GameDbContext> _unitOfWork;
    private readonly IMediator _mediator;

    public GameHub(GameDbContext dbContext, IHttpContextAccessor context, PlayerRepository playerRepository, IUnitOfWork<GameDbContext> unitOfWork, IMediator mediator): base()
    {
        _dbContext = dbContext;
        _context = context;
        _playerRepository = playerRepository;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }
    public async Task SendRoomInfo(Room room)
    {
        await Clients.All.SendAsync("NewRoomInfo", room);
    }
    public async Task SendNewGameState(string groupName, Game game)
    {
        await Clients.Group(groupName).SendAsync("CommunicateMarketData", game);
    }
    public async Task SendDeletedRoomInfo(Guid roomId)
    {
        await Clients.All.SendAsync("DeletedRoomInfo", roomId);
    }
    
    public async Task AddToGroup(string connectionId, string groupName)     
        => await Groups.AddToGroupAsync(connectionId, groupName); 
    public async Task RemoveFromGroup(string connectionId, string groupName)     
        => await Groups.RemoveFromGroupAsync(connectionId, groupName);
    
    [AuthorizeRoute()]
    public override async Task OnConnectedAsync() { 
        if(_context.HttpContext!.Items["User"] == null) throw new AuthenticationException("Authentication error!");

        var userid = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;
        var connectionId = Context.ConnectionId;

        var user = await _playerRepository.GetByIdAsync(userid);
        if (user is not null)
        {
            user.SetConnectionId(connectionId);
        }
        else
        {
            _playerRepository.Add(new Player(userid, connectionId));
        }

        await _unitOfWork.CommitAsync();
    }

    [AuthorizeRoute()]
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if(_context.HttpContext!.Items["User"] == null) throw new AuthenticationException("Authentication error!");

        var userid = ((AccessToken) _context.HttpContext.Items["User"])!.UserId;

        var user = await _playerRepository.GetByIdAsync(userid);
        if (user is not null)
        {
            _dbContext.Players.Remove(user);
        }

        await _unitOfWork.CommitAsync();
    }
}