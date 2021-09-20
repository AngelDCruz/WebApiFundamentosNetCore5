using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase { 

        private readonly IConfiguration _configuration;

        public ConfiguracionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult ObtenerConeccionDatos()
        {
            return Ok(_configuration["connectionStrings:defaultConnection"]);
        }
    }
}
