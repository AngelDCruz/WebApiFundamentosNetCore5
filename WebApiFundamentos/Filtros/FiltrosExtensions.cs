using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Filtros
{
    public static class FiltrosExtensions
    {
        public static IServiceCollection AddFiltros(this IServiceCollection services) {
            
            services.AddTransient<FiltroAccion>();

            return services;
        }
    }
}
