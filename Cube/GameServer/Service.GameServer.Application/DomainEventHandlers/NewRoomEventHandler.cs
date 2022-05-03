using System.Threading;
using System.Threading.Tasks;
using Infrastructure.DDD.Interface;
using Service.GameServer.Domain.Events;

namespace Service.GameServer.Application.DomainEventHandlers
{
    public class NewRoomEventHandler :
        IDomainEventHandler<NewRoomEvent>
    {

        public NewRoomEventHandler()
        {
        }

        public async Task Handle(NewRoomEvent notification, CancellationToken cancellationToken)
        {
        }
    }
}