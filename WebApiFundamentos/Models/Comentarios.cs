using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Models
{
    public class Comentarios
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public int LibroId { get; set; }
        public Libros Libro { get; set; }

        public Guid UsuarioId {get; set; }

        public IdentityUser Usuario { get; set; }
    }
}
