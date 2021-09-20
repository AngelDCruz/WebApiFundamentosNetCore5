using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFundamentos.Validators
{
    public class PrimerLetraMayuscula : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString())) return ValidationResult.Success;

            string letra = value.ToString()[0].ToString();

            if (letra != letra.ToUpper()) return new ValidationResult("La primer letra del nombre debe ser mayúscula");

            return ValidationResult.Success;
        }
    }
}
