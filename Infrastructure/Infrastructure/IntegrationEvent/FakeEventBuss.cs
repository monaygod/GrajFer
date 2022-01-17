using Infrastructure.IntegrationEvent.Interface;

namespace Infrastructure.IntegrationEvent
{
    public class FakeEventBuss : IEventBus
    {
        public void Publish(IntegrationEvent @event)
        {
            System.Console.WriteLine(@event.ToString());
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }
    }
}