using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IntegrationEvent.Interface
{
    public interface IIntegrationEventDispatcher<T> where T: DbContext
    {
        public void DispatchIntegrationEvent();
    }
}