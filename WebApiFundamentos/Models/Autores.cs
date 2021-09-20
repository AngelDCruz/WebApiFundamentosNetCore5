using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiFundamentos.Validators;

namespace WebApiFundamentos.Models
{
    public class Autores: IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} debe contener máximo {1} caracteres")]
        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        [NotMapped]
        [EdadMayorValidator]
        public int Edad {get; set; }

        public List<AutorLibro> AutorLibro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!string.IsNullOrEmpty(Apellidos))
            {
                string letra = Apellidos[0].ToString();

                if(letra != letra.ToUpper())
                {
                    yield return new ValidationResult(
                        "La primera letra del apellido debe ser mayúscula",
                        new string [] { nameof(Nombre) }
                     );
                }
            }
        }
    }
}
