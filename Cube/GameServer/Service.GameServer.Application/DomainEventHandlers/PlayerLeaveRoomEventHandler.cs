using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Service.GameServer.Application.Hubs;
using Service.GameServer.Domain.Events;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.DomainEventHandlers;

public class PlayerLeaveRoomEventHandler:
    IDomainEventHandler<PlayerLeaveRoomEvent>
{
    private readonly GameHub _gameHub;
    private readonly RoomRepository _roomRepository;

    public PlayerLeaveRoomEventHandler(GameHub gameHub, RoomRepository roomRepository)
    {
        _gameHub = gameHub;
        _roomRepository = roomRepository;
    }

    public async Task Handle(PlayerLeaveRoomEvent notification, CancellationToken cancellationToken)
    {
        Room room = await _roomRepository.GetByIdAsync(notification.RoomId);
        Task hubTask = _gameHub.SendRoomInfo(room);
    }
}