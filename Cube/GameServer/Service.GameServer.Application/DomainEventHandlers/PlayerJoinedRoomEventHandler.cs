using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Service.GameServer.Application.Hubs;
using Service.GameServer.Domain.Events;
using Service.GameServer.Domain.RoomAggregate;
using Service.GameServer.Repository;

namespace Service.GameServer.Application.DomainEventHandlers;

public class PlayerJoinedRoomEventHandler:
    IDomainEventHandler<PlayerJoinedRoomEvent>
{
    private readonly GameHub _gameHub;
    private readonly RoomRepository _roomRepository;

    public PlayerJoinedRoomEventHandler(GameHub gameHub, RoomRepository roomRepository)
    {
        _gameHub = gameHub;
        _roomRepository = roomRepository;
    }

    public async Task Handle(PlayerJoinedRoomEvent notification, CancellationToken cancellationToken)
    {
        Room room = await _roomRepository.GetByIdAsync(notification.RoomId);
        Task hubTask = _gameHub.SendRoomInfo(room);
    }
}