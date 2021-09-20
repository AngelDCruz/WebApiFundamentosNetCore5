using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        [HttpGet]
        [ResponseCache(Duration = 10)]
        public IActionResult ObtenerCadenaAleatoria()
        {
            return Ok(new { 
                id = Guid.NewGuid()
            });
        }
    }
}
