using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.IntegrationEvent
{
    public sealed class RabbitPersistentConnection : IRabbitPersistentConnection, IDisposable
    {
        private readonly IConnectionFactory _connectionFactory;
        IConnection _connection;
        bool _disposed;

        public RabbitPersistentConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            _connection?.Dispose();
        }

        public bool IsConnected
        {
            get { return _connection != null && _connection.IsOpen && !_disposed; }
        }

        public bool TryConnect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;

            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            
            TryConnect();
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;

            TryConnect();
        }
        
    }
}