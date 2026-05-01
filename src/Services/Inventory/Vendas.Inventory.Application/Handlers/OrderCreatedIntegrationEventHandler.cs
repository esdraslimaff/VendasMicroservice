using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.EventBus.Abstractions;
using Vendas.IntegrationEvents;
using Vendas.Inventory.Application.Interfaces.Servicos;

namespace Vendas.Inventory.Application.Handlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly IProductService _productService;

        public OrderCreatedIntegrationEventHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task Handle(OrderCreatedIntegrationEvent @event)
        {
            Console.WriteLine($"[LOG] Pedido recebido: {@event.OrderId}");

            foreach (var item in @event.Items)
            {
                await _productService.UpdateStockAsync(
                    item.ProductId,
                    item.Quantity
                );

                Console.WriteLine($"[LOG] Estoque atualizado: {item.ProductId} ({item.Quantity})");
            }
        }
    }
}
