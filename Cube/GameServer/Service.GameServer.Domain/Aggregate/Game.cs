using Infrastructure.DDD;
using Infrastructure.DDD.Interface;

namespace Service.GameServer.Domain.UserAggregate
{
    public class Game : Entity, IAggregateRoot
    {
        public static Game Create()
        {
            return new Game
            {
            };
        }
    }
}