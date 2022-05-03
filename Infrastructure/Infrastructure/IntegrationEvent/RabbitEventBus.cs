using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Infrastructure.IntegrationEvent.Interface;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.IntegrationEvent
{
    public class RabbitEventBus: IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRabbitPersistentConnection _connection;
        private readonly IModel _consumerChanel;
        private List<EventingBasicConsumer> _consumers; //TODO
  
        public RabbitEventBus(IServiceProvider serviceProvider, IRabbitPersistentConnection connection)
        {
            _serviceProvider = serviceProvider;
            _connection = connection;
            connection.TryConnect();
            _consumerChanel = connection.CreateModel();
            _consumers = new List<EventingBasicConsumer>();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            using (var chanel = _connection.CreateModel())
            {
                chanel.QueueDeclare(queue: @event.GetType().Name,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = JsonSerializer.Serialize(@event);
                var body = Encoding.UTF8.GetBytes(message);

                chanel.BasicPublish(exchange: "",
                    routingKey: @event.GetType().Name,
                    basicProperties: null,
                    body: body);
            }
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            _consumerChanel.QueueDeclare(queue: typeof(T).Name,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                var scope = _serviceProvider.CreateScope();
                var integrationEventHandler = scope.ServiceProvider.GetService<TH>();
                var consumer = new EventingBasicConsumer(_consumerChanel);
                
                consumer.Received += (sender, args) =>
                {
                    try
                    {
                        var ddd = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(args.Body.ToArray()));
                        integrationEventHandler?.Handle(JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(args.Body.ToArray())));
                    }
                    finally
                    {
                        _consumerChanel.BasicAck(args.DeliveryTag, false);
                    }
                };
                _consumers.Add(consumer);
                _consumerChanel.BasicConsume(queue: typeof(T).Name,
                    autoAck: false,
                    consumer: consumer);
        }

        public void Unsubscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new System.NotImplementedException();
        }
    }
}