using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Filtros
{
    public class FiltroExcepcion : ExceptionFilterAttribute
    {

        private readonly ILogger<FiltroExcepcion> _logger;

        public FiltroExcepcion(ILogger<FiltroExcepcion> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogInformation(context.Exception, context.Exception.Message);

            base.OnException(context);
        }

    }
}
