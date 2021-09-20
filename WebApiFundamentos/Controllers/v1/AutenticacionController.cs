using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Servicios;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly IConfiguration _configuration;
        private readonly TokenServices _tokenServices;

        public AutenticacionController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signManager,
            IConfiguration configuration,
            TokenServices tokenServices
         )
        {
            _userManager = userManager;
            _signManager = signManager;
            _configuration = configuration;
            _tokenServices = tokenServices;
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] CrearUsuario usuario)
        {

            IdentityUser usExiste = await _userManager.FindByNameAsync(usuario.Usuario);
            usExiste =  await _userManager.FindByEmailAsync(usuario.Email);

            if (usExiste != null) return BadRequest("El nombre de usuario o email no estan disponibles");

            var respuesta = await _userManager.CreateAsync(
                new IdentityUser {  UserName = usuario.Usuario, Email=usuario.Email }, 
                usuario.Password
            );



            if (!respuesta.Succeeded) BadRequest(respuesta.Errors);

            return Ok(usExiste);
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaTokenDTO>> autenticacion([FromBody] AutenticacionDTO auth)
        {

            IdentityUser user = await _userManager.FindByEmailAsync(auth.Email);
            bool estaAutenticado = await _userManager.CheckPasswordAsync(user, auth.Password);

            if (user == null && !estaAutenticado) return BadRequest("usuario o contraseña incorrectos");

            return Ok(_tokenServices.GenerarToken(user));
        }


        [HttpGet("leer-token/{token}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<JwtSecurityToken> leerToken([FromRoute] string token)
        {
            return Ok(_tokenServices.LeerToken(token));
        }


        public class CrearUsuario {

            [Required(ErrorMessage = "El  {0} es requerido")]
            [StringLength(maximumLength:30, ErrorMessage = "El  {0} debe de contener máximo {1} caracteres")]
            public string Usuario { get; set; }

            [Required(ErrorMessage = "El {0} es requerido")]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(maximumLength:50, ErrorMessage = "El {0} debe de contener máximo {1} caracteres")]
            public string Password { get; set; }
        }

    }
}
