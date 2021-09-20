using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiFundamentos.DTOs
{
    public class RespuestaTokenDTO
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }

    public class AutenticacionDTO
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "El {0} es requerido")]
        public string Password { get; set; }
    }
}
