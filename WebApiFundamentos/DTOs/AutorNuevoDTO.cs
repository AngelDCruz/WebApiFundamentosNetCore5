using System.ComponentModel.DataAnnotations;
using WebApiFundamentos.Validators;

namespace WebApiFundamentos.DTOs
{
    public class AutorNuevoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength:150, ErrorMessage = "El campo {0} debe de contener máximo {1} caracteres")]
        [PrimerLetraMayuscula]
        public string NombreAutor{ get; set; }
    }
}
