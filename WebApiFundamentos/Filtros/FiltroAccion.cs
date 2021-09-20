using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Filtros
{
    public class FiltroAccion : IActionFilter
    {

        private readonly ILogger<FiltroAccion> _logger;
        public FiltroAccion(ILogger<FiltroAccion> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Se ejecuto el filtro antes de entrar a la petición");
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Se ejecuto el filtro despues de entrar a la petición");
        }

    }
}
