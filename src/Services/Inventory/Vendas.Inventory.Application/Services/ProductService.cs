using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.Application.Dtos;
using Vendas.Inventory.Application.Interfaces.Servicos;
using Vendas.Inventory.Domain.Entities;
using Vendas.Inventory.Domain.Interfaces.Repository;

namespace Vendas.Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) => _repository = repository;

        public async Task<Guid> CreateAsync(CreateProductRequest request)
        {
            var product = new Product(request.Name, request.Description, request.Price, request.StockQuantity);

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return product.Id;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.Price,
                p.StockQuantity,
                p.Active
            ));

        }
    }
}
