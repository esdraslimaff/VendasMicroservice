using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vendas.Inventory.Application.Dtos;
using Vendas.Inventory.Application.Interfaces.Servicos;

namespace Vendas.Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IProductService _service;
        public InventoryController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest request)
        {
            var id = await _service.CreateAsync(request);
            return Ok(id);
        }
    }
}
