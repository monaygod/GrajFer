using System;
using RabbitMQ.Client;

namespace Infrastructure.IntegrationEvent
{
    public interface IRabbitPersistentConnection : IDisposable
    {
        bool IsConnected { get; }

        bool TryConnect();

        IModel CreateModel();
    }
}