using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendas.EventBus;
using Vendas.EventBus.Abstractions;
using Vendas.IntegrationEvents;
using Vendas.Orders.Application.Interfaces.Services;
using Vendas.Orders.Application.Services;

namespace Vendas.Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IEventBus _eventBus;

        public OrderController(IOrderService orderService, IEventBus eventBus)
        {
            _orderService = orderService;
            _eventBus = eventBus;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "API Order funcionando!" });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var orderId = await _orderService.CreateOrderAsync(request);

            return CreatedAtAction(nameof(GetById), new { id = orderId }, new { id = orderId });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);

            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPost("testemensagem")]
        public async Task<IActionResult> CreateOrder()
        {
            var orderId = Guid.NewGuid();
            var integrationEvent = new OrderCreatedIntegrationEvent(
                orderId,
                99.90m,
                new List<OrderItemDto> { new(Guid.NewGuid(), 1) }
            );

            // PUBLICANDO!
            await _eventBus.PublishAsync(integrationEvent);

            return Ok(new { Message = "Evento de Pedido enviado!", Id = orderId });
        }
    }
}
