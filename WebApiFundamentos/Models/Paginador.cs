using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Models
{
    public class Paginador
    {

        private readonly int LimiteRegistrosPagina = 50;

        private int RegistrosPorPagina = 3;

        public int Pagina { get; set; }

        public int RegistrosPagina
        {
            get { return RegistrosPorPagina;  }
            set
            {
                RegistrosPorPagina = (value > LimiteRegistrosPagina) ? RegistrosPorPagina : value;
            }
        }
    }
}
