using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.Application.Dtos;
using Vendas.Inventory.Application.Services;
using Vendas.Inventory.Domain.Entities;
using Vendas.Inventory.Domain.Interfaces.Repository;

namespace Vendas.Inventory.Tests.Application
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Deve_chamar_repositorio_ao_criar_produto()
        {
            var repositoryMock = new Mock<IProductRepository>();
            var service = new ProductService(repositoryMock.Object);

            var request = new CreateProductRequest("Produto Teste", "descricao", 10, 1);

            var result = await service.CreateAsync(request);

            repositoryMock.Verify(
                r => r.AddAsync(It.IsAny<Product>())
                Times.Once()
            );

            repositoryMock.Verify(
                r => r.SaveChangesAsync(),
                Times.Once()
            );
        }
    }
}

