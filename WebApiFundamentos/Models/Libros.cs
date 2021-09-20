using System;
using System.Collections.Generic;

namespace WebApiFundamentos.Models
{
    public class Libros
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public List<Comentarios> Comentarios { get; set; }

        public List<AutorLibro> AutorLibro { get; set; }

    }
}
