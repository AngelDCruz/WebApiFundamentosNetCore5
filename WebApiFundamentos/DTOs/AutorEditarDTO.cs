using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.Validators;

namespace WebApiFundamentos.DTOs
{
    public class AutorEditarDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [PrimerLetraMayuscula]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} debe contener máximo {1}")]
        public string Nombre { get; set; }

    }
}
