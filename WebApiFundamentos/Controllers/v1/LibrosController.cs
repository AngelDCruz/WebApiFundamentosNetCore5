using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;
using WebApiFundamentos.Seguridad;

namespace WebApiFundamentos.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibrosController : ControllerBase
    {
        private readonly ILogger<LibrosController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _map;

        public LibrosController(
            ILogger<LibrosController> logger,
            ApplicationDbContext context,
            IMapper map
         )
        {
            _logger = logger;
            _context = context;
            _map = map;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<LibrosDTO>>> getAll()
        {
            _logger.LogInformation("Log informativo del backend");

            List<Libros> libros = await _context.Libros
                .Include(x => x.Comentarios)
                .Include(x => x.AutorLibro)
                .ThenInclude(y => y.Autor).ToListAsync();

            if (libros == null) return NoContent();

            List<LibrosDTO> librosDTO = _map.Map<List<LibrosDTO>>(libros);

            return Ok(librosDTO);
        }

        [HttpGet("{id:int}", Name = "getById")]
        [Authorize(Policy = ClaimsSistema.Administrador)]
        public async Task<ActionResult<LibrosDTO>> getById(int id)
        {
            Libros libro = await _context.Libros
                .Include(x => x.Comentarios)
                .Include(x => x.AutorLibro)
                .ThenInclude(x => x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null) return NotFound("El libro no se ha encontrado");

            return Ok(_map.Map<LibrosDTO>(libro));
        }

        [HttpPost]
        public async Task<ActionResult> create([FromBody] LibroNuevoDTO libroDTO)
        {

            List<int> autoresId = await _context.Autores.Where(x => libroDTO.AutoresId.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            bool isLibro = await _context.Libros.AnyAsync(x => x.Nombre == libroDTO.Nombre);

            if (libroDTO.AutoresId.Count != autoresId.Count) return BadRequest("Uno de los autores no existe");
            if (isLibro) return BadRequest("El libro ya se ha registrado anteriormente");

            Libros libro = _map.Map<Libros>(libroDTO);

            _context.Add(libro);
            await _context.SaveChangesAsync();

            var libroDTORespuesta = _map.Map<LibrosDTO>(libro);

            return CreatedAtRoute("getById", new { id = libro.Id}, libroDTORespuesta);
        } 
        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LibrosDTO>> put([FromBody] LibroEditarDTO libroDTO, [FromRoute] int id)
        {

            if (id != libroDTO.Id) return BadRequest("El libro no es valido");

            bool isLibro= await _context.Libros.AnyAsync(x => x.Id == libroDTO.Id);

            if (!isLibro) return NotFound("El libro no existe");

            Libros libro = _map.Map<Libros>(libroDTO);

            _context.Update(libro);
            await _context.SaveChangesAsync();

            return Ok(_map.Map<LibrosDTO>(libro));
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> patch([FromRoute] int id,  [FromBody] JsonPatchDocument<LibroPatchDTO> libroPatchDTO)
        {

            if (libroPatchDTO == null) return BadRequest("Libro no valido");

            Libros libro = await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null) return NotFound("Libro no encontrado");

            LibroPatchDTO libroPatch = _map.Map<LibroPatchDTO>(libro);

            libroPatchDTO.ApplyTo(libroPatch, ModelState);

            if (!TryValidateModel(libroPatch)) return BadRequest(ModelState);

            _map.Map(libroPatch, libro);

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete([FromRoute] int id)
        {
            Libros libro = await _context.Libros.FirstOrDefaultAsync(x => x.Id == id);

            if (libro == null) return NotFound("Libro no encontrado");

            _context.Remove(libro);

            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
