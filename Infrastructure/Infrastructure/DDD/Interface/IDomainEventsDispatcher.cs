using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.DDD.Interface
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
        Task DispatchEventsAsync(ICollection<Entity> entities);
    }
}