using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vendas.Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "API Order funcionando!" });
        }
    }
}
