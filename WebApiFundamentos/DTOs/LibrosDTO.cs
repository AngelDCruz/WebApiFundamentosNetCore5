using System.Collections.Generic;

namespace WebApiFundamentos.DTOs
{
    public class LibrosDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<ComentariosDTO> Comentarios { get; set; }

        public List<AutorDTO> Autores { get; set; }
    }
}
