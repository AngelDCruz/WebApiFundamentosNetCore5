using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;
using WebApiFundamentos.Servicios;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ComentariosController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _map;
        private readonly UsuarioAutenticadoServices _usuarioAutenticadoServices;

        public ComentariosController(
            ApplicationDbContext context,
            UsuarioAutenticadoServices usuarioAutenticadoServices,
            IMapper map
         )
        {
            _context = context;
            _map = map;
            _usuarioAutenticadoServices = usuarioAutenticadoServices;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<ComentariosDTO>>> getById([FromRoute] int id)
        {
            Comentarios comentarios = await _context.Comentarios.FirstOrDefaultAsync(x => x.Id == id);

            if (comentarios == null) return NotFound("El comentario no existe");

            return Ok(_map.Map<List<ComentariosDTO>>(comentarios));
        }


        [HttpPost]
        public async Task<ActionResult<ComentariosDTO>> create([FromBody] ComentarioNuevoDTO comentarioDTO)
        {
            IdentityUser usuario = await _usuarioAutenticadoServices.ObtenerUsuario();

            bool isExiste = await _context.Libros.AnyAsync(x => x.Id == comentarioDTO.IdLibro);

            if (!isExiste) return NotFound("Libro no encontrado");

            Comentarios comentario = _map.Map<Comentarios>(comentarioDTO);
            comentario.UsuarioId = Guid.Parse(usuario.Id);

            await _context.AddAsync(comentario);
            await _context.SaveChangesAsync();

            return Ok(_map.Map<ComentariosDTO>(comentario));

        }

    }
}
