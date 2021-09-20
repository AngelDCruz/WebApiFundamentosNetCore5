using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFundamentos.DTOs
{
    public class LibroEditarDTO
    { 
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} requiere como máximo {1} caracteres")]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
        
    }
}
