using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.Application.Dtos;

namespace Vendas.Inventory.Application.Interfaces.Servicos
{
    public interface IProductService
    {
        Task<Guid> CreateAsync(CreateProductRequest request);
        Task<IEnumerable<ProductResponse>> GetAllAsync();
    }
}
