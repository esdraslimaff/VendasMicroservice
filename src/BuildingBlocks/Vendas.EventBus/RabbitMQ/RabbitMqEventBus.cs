using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Vendas.EventBus.Abstractions;
using Vendas.EventBus.Events;

namespace Vendas.EventBus.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private const string ExchangeName = "vendas_event_bus";

        public RabbitMqEventBus(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync(IntegrationEvent @event)
        {
            using var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Topic);

            var message = JsonSerializer.Serialize(@event, @event.GetType());
            var body = Encoding.UTF8.GetBytes(message);

            var routingKey = @event.GetType().Name;

            await channel.BasicPublishAsync(
                exchange: ExchangeName,
                routingKey: routingKey,
                body: body);
        }

        public void Subscribe<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }
    }
}
