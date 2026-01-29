using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vendas.Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "API Orders funcionando!" });
        }
    }
}
