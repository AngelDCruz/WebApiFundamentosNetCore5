using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Servicios
{
    public static class ServicesExtensions
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHostedService<GrabarTextoServices>();

            services.AddTransient<TokenServices>();

            services.AddScoped<UsuarioAutenticadoServices>();

            services.AddSingleton<HashServices>();

            return services;
        }

    }
}
