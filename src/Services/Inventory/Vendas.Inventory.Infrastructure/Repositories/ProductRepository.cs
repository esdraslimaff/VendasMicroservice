using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.Domain.Entities;
using Vendas.Inventory.Domain.Interfaces.Repository;
using Vendas.Inventory.Infrastructure.Data.Context;

namespace Vendas.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryContext _context;

        public ProductRepository(InventoryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetByIdAsync(Guid id)
            => await _context.Products.FindAsync(id);

        public async Task AddAsync(Product product)
            => await _context.Products.AddAsync(product);

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
