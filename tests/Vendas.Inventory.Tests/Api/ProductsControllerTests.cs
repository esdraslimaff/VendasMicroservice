using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.API.Controllers;
using Vendas.Inventory.Application.Dtos;
using Vendas.Inventory.Application.Interfaces.Servicos;
using Vendas.Inventory.Application.Services;
using Vendas.Inventory.Domain.Entities;

namespace Vendas.Inventory.Tests.Api
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task Deve_retornar_ok_ao_criar_produto()
        {
            var serviceMock = new Mock<IProductService>();
            var controller = new InventoryController(serviceMock.Object);

            var productRequest = new CreateProductRequest("Produto A", "descricao do produto A", 10.30m, 20);

            var result = await controller.Post(productRequest);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
