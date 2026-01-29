using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Orders.Domain.Entities;

namespace Vendas.Orders.Domain.Interfaces.Repository
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}
