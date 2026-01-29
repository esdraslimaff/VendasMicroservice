using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Inventory.Application.Dtos
{
    public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);

    public record ProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    int StockQuantity,
    bool Active
);
}
