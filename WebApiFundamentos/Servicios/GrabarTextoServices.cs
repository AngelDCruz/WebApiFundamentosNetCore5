using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiFundamentos.Servicios
{
    public class GrabarTextoServices : IHostedService
    {

        private readonly IWebHostEnvironment _env;
        private Timer _timer;

        public GrabarTextoServices(
            IWebHostEnvironment env   
         )
        {
            _env = env;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ProcesoEnEjecucion,  null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            EscribirArchivoTexto("Proceso Iniciado");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {

            EscribirArchivoTexto("Proceso Finalizado");

            return Task.CompletedTask;
        }

        private void ProcesoEnEjecucion(object state)
        {
            EscribirArchivoTexto($@"Proceso ejecutandose - {DateTime.Now.ToString("dd/MM/yyyy")}");
        }

        private void EscribirArchivoTexto(string mensaje)
        {
            string ruta = $@"{_env.ContentRootPath}\wwwroot\observador.txt";

            using (StreamWriter wr = new StreamWriter(ruta, append: true))
            {
                wr.WriteLine(mensaje);
            }
        }
    }
}
