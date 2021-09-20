using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Controllers.V2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _map;

        public LibrosController(
            ApplicationDbContext context,
            IMapper map
         )
        {
            _context = context;
            _map = map;
        }

        /// <sumary>
        ///  Obtener Registros de Libros 
        /// </sumary>
        /// <returns>Returna un listado de Libros</returns>
        /// <response code="200">Retorna listado de Libros</response>
        /// <response code="204">No hay registros</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<LibrosDTO>>> getAll()
        {
            List<Libros> libros = await _context.Libros
                .Include(x => x.Comentarios)
                .Include(x => x.AutorLibro)
                .ThenInclude(y => y.Autor).ToListAsync();

            if (libros == null) return NoContent();

            List<LibrosDTO> librosDTO = _map.Map<List<LibrosDTO>>(libros);

            return Ok(librosDTO);
        }
    }
}
