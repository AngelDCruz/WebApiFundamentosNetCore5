using System.ComponentModel;

namespace WebApiFundamentos.DTOs
{
    public class PaginadorDTO
    {
        [DefaultValue(1)]
        public int Pagina { get; set; }

        [DefaultValue(10)]
        public int RegistrosPorPagina { get; set; }
    }
}
