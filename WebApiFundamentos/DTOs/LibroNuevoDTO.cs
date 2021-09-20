using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiFundamentos.DTOs
{
    public class LibroNuevoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} requiere máximo {1} caracteres")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public List<int> AutoresId { get; set; }
    }
}
