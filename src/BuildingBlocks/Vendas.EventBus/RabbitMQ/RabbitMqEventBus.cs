using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Vendas.EventBus.Abstractions;
using Vendas.EventBus.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Vendas.EventBus.RabbitMQ
{
    public class RabbitMqEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private const string ExchangeName = "vendas_event_bus";
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqEventBus(IConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
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

        public async Task SubscribeAsync<T, TH>() where T : IntegrationEvent where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var queueName = $"inventory_{eventName}_queue";

            // Criando o canal na v7
            var channel = await _connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: queueName, durable: true, exclusive: false, autoDelete: false);
            await channel.QueueBindAsync(queue: queueName, exchange: "vendas_event_bus", routingKey: eventName);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"\n[RABBITMQ] Mensagem recebida na fila {queueName}!");
                Console.WriteLine($"[RABBITMQ] Conteúdo: {message}");

                try
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var integrationEvent = JsonSerializer.Deserialize<T>(message, options);

                    if (integrationEvent == null)
                    {
                        Console.WriteLine("[ERRO RABBITMQ] O JSON veio vazio ou não pôde ser convertido.");
                        return;
                    }

                    Console.WriteLine($"[RABBITMQ] Evento {eventName} desserializado com sucesso! (ID do Pedido: {integrationEvent.Id})");

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var handler = scope.ServiceProvider.GetService<TH>();

                        if (handler == null)
                        {
                            Console.WriteLine($"[ERRO RABBITMQ] Handler {typeof(TH).Name} não foi encontrado na Injeção de Dependência!");
                            return;
                        }

                        await handler.Handle(integrationEvent);
                        Console.WriteLine("[RABBITMQ]  Processamento do Handler concluído!");
                    }

                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n[ERRO FATAL RABBITMQ]: {ex.Message}");
                    Console.WriteLine($"StackTrace: {ex.StackTrace}");

                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            await channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
            Console.WriteLine($"[RABBITMQ] Inscrito e aguardando eventos do tipo: {eventName}");
        }
    }
}
