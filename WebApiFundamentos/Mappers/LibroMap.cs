using AutoMapper;
using System.Collections.Generic;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Mappers
{
    public class LibroMap : Profile
    {
        public LibroMap()
        {
            CreateMap<Libros, LibrosDTO>()
                .ForMember(libro => libro.Id, libroDTO => libroDTO.MapFrom(propiedad => propiedad.Id))
                .ForMember(libro => libro.Nombre, libroDTO => libroDTO.MapFrom(propiedad => propiedad.Nombre))
                .ForMember(libro => libro.Autores, libroDTO => libroDTO.MapFrom(AutoresMap))
                .ReverseMap();
            
            CreateMap<LibroNuevoDTO, Libros>()
                .ForMember(libro => libro.Nombre, libroDTO => libroDTO.MapFrom(propiedad => propiedad.Nombre))
                .ForMember(libro => libro.AutorLibro, libroDTO => libroDTO.MapFrom(AutorLibroMap))
                .ReverseMap();

              CreateMap<Libros, LibroEditarDTO>()
                 .ForMember(libro => libro.Id, libroDTO => libroDTO.MapFrom(propiedad => propiedad.Id))
                .ForMember(libro => libro.Nombre, libroDTO => libroDTO.MapFrom(propiedad => propiedad.Nombre))
                .ReverseMap();

            CreateMap<Libros, LibroPatchDTO>()
              .ForMember(libroPatch => libroPatch.Nombre, libroEDTO => libroEDTO.MapFrom(pro => pro.Nombre))
              .ForMember(libroPatch => libroPatch.FechaPublicacion, libroEDTO => libroEDTO.MapFrom(pro => pro.FechaCreacion))
              .ReverseMap();
        }

        private List<AutorLibro> AutorLibroMap(LibroNuevoDTO libroDTO, Libros libro)
        {
            List<AutorLibro> autorLibro = new List<AutorLibro>();

            if (libroDTO.AutoresId.Count <= 0) return autorLibro;

            foreach (var autorId in libroDTO.AutoresId) 
                autorLibro.Add(new AutorLibro() { AutorId = autorId, LibroId = libro.Id });

            return autorLibro;
        }

        private List<AutorDTO> AutoresMap(Libros libro, LibrosDTO librosDTO)
        {
            List<AutorDTO> autoresDTO = new List<AutorDTO>();

            if (libro.AutorLibro.Count <= 0) return autoresDTO;

            foreach(AutorLibro valor in libro.AutorLibro)
            {
                AutorDTO autorDTO = new AutorDTO();

                if (valor.Autor == null) continue;
                
                 autorDTO.Id = valor.Autor.Id;
                 autorDTO.NombreCompleto = valor.Autor.Nombre;
               
                autoresDTO.Add(autorDTO);
            }

            return autoresDTO;
        }
    }
}
