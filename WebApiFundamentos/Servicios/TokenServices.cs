using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;

namespace WebApiFundamentos.Servicios
{
    public class TokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RespuestaTokenDTO GenerarToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>()
                {
                    new Claim("usuario", user.UserName),
                    new Claim("email", user.Email)
                };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
            var firma = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddMinutes(2);
            var securityToken = new JwtSecurityToken(
                issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: firma
             );

            return new RespuestaTokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }

        public JwtSecurityToken LeerToken(string token)
        {
            var tokenManejador = new JwtSecurityTokenHandler();
            JwtSecurityToken tokenDesencriptado = tokenManejador.ReadJwtToken(token);

            return tokenDesencriptado;
        }
    }
}
