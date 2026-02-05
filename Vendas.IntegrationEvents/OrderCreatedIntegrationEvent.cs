using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.EventBus.Events;

namespace Vendas.IntegrationEvents
{
    public record OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; init; }
        public decimal TotalValue { get; init; }
        public List<OrderItemDto> Items { get; init; }

        public OrderCreatedIntegrationEvent(Guid orderId, decimal totalValue, List<OrderItemDto> items)
        {
            OrderId = orderId;
            TotalValue = totalValue;
            Items = items;
        }
    }

    public record OrderItemDto(Guid ProductId, int Quantity);
}
