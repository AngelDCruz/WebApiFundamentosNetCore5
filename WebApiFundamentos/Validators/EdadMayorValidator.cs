using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Validators
{
    public class EdadMayorValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valor = value.ToString();

            if (string.IsNullOrEmpty(valor) && value == null) return ValidationResult.Success;

            if (int.Parse(valor) < 18) return new ValidationResult("El autor es menor de edad");

            return ValidationResult.Success;
        }
    }
}
