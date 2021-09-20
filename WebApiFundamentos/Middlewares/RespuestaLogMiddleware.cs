using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace WebApiFundamentos.Middlewares
{
    public class RespuestaLogMiddleware
    {

        private readonly ILogger<RespuestaLogMiddleware> _logger;
        private readonly RequestDelegate _siguiente;

        public RespuestaLogMiddleware(
            ILogger<RespuestaLogMiddleware> logger,
            RequestDelegate siguiente
        )
        {
            _logger = logger;
            _siguiente = siguiente;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cuerpoRespuestaOrigen = contexto.Response.Body;
                contexto.Response.Body = ms;

                await _siguiente(contexto);

                ms.Seek(0, SeekOrigin.Begin);
                string respuesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);

                await ms.CopyToAsync(cuerpoRespuestaOrigen);
                contexto.Response.Body = cuerpoRespuestaOrigen;

                _logger.LogInformation(respuesta);
            }
        }

    }
}
