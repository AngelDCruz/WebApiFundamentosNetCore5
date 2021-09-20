using System.ComponentModel.DataAnnotations;

namespace WebApiFundamentos.DTOs
{
    public class ComentarioNuevoDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdLibro { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Contenido { get; set; }
    }
}
