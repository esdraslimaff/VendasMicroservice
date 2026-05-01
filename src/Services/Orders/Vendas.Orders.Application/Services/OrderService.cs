using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.EventBus.Abstractions;
using Vendas.IntegrationEvents;
using Vendas.Orders.Application.Interfaces.Services;
using Vendas.Orders.Domain.Entities;
using Vendas.Orders.Domain.Interfaces.Repository;

namespace Vendas.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEventBus _eventBus;

        public OrderService(IOrderRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderRequest request)
        {
            var order = new Order(request.CustomerId);

            foreach (var item in request.Items)
            {
                order.AddItem(
                    item.ProductId,
                    item.ProductName,
                    item.UnitPrice,
                    item.Quantity);
            }

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();

            var @event = new OrderCreatedIntegrationEvent(
                order.Id,
                order.TotalAmount,
                request.Items.Select(i => new OrderItemDto(i.ProductId, i.Quantity)).ToList()
            );

            await _eventBus.PublishAsync(@event);

            return order.Id;
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(Guid id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null) return null;

            return new OrderResponse(
                order.Id,
                order.CustomerId,
                order.TotalAmount,
                order.Status.ToString(),
                order.CreatedAt
            );
        }
    }
}
