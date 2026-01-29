using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Orders.Domain.Enums;

namespace Vendas.Orders.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; private set; }

        private readonly List<OrderItem> _items = new();
        public IReadOnlyCollection<OrderItem> Items => _items;

        public OrderStatus Status { get; private set; }

        public Order(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            var item = new OrderItem(productId, productName, unitPrice, quantity);
            _items.Add(item);
        }

        public decimal TotalAmount => _items.Sum(x => x.UnitPrice * x.Quantity);
    }
}
