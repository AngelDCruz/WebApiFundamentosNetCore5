using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using WebApiFundamentos.Servicios;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashController : ControllerBase
    {

        private readonly HashServices _hashServices;
        private readonly IDataProtector _dataProtector;

        public HashController(HashServices hashServices, IDataProtectionProvider dataProtector)
        {
            _hashServices = hashServices;
            _dataProtector = dataProtector.CreateProtector("121212312312");
        }

        [HttpGet]
        public object EncriptarHash()
        {
            var nombre = "Angel Reynaldo";

            var encriptar1 = _hashServices.Hash(nombre);
            var encriptar2 = _hashServices.Hash(nombre);

            var dataProtector = _dataProtector.Protect(nombre);

            return new
            {
                Cadena = nombre,
                encriptado1 = encriptar1,
                encriptado2 = encriptar2,
                encriptamientoProtector = dataProtector,
                desencriptamientoProtector = _dataProtector.Unprotect(dataProtector)
            };
        }

    }
}
