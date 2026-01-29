using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Inventory.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public bool Active { get; private set; }

        public Product(string name, string description, decimal price, int stockQuantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            Active = true;

            if (price <= 0) throw new ArgumentException("Preço inválido");
        }

        public void DebitarEstoque(int quantidade)
        {
            if (StockQuantity < quantidade) throw new Exception("Estoque insuficiente");
            StockQuantity -= quantidade;
        }
    }
}
