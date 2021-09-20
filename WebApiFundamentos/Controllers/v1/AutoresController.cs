using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Filtros;
using WebApiFundamentos.Models;
using WebApiFundamentos.Seguridad;

namespace WebApiFundamentos.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AutoresController(
            ApplicationDbContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(FiltroAccion))]
        public async Task<ActionResult<List<AutorDTO>>> getAll([FromQuery] PaginadorDTO pagDTO)
        {
            //throw new NotImplementedException();
            Paginador paginador = _mapper.Map<Paginador>(pagDTO);
            var lstAutores = await _context.Autores.AsQueryable().Paginar(paginador).ToListAsync();

            if (lstAutores == null) return NoContent();

            return Ok(_mapper.Map<List<AutorDTO>>(lstAutores));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorDTO>> getById([FromRoute] int id)
        {
            Autores autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null) return NotFound("Usuario no encontrado");

            return Ok(_mapper.Map<AutorDTO>(autor));
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<AutorDTO>> create([FromBody] AutorNuevoDTO autorDTO)
        {

            Autores autor = _mapper.Map<Autores>(autorDTO);

            _context.Add(autor);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<AutorDTO>(autor));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AutorDTO>> update([FromBody] AutorEditarDTO autorDTO,  int id)
        {
            if(id != autorDTO.Id) return BadRequest("El usuario que ingresaste no es válido");

            Autores autor = _mapper.Map<Autores>(autorDTO);

            _context.Update(autor);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<AutorDTO>(autor));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> delete(int id)
        {
            bool isAutor = await _context.Autores.AnyAsync(x => x.Id == id);

            if (!isAutor) return NotFound("Author not find");

            _context.Remove(new Autores() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }   
    }
}
