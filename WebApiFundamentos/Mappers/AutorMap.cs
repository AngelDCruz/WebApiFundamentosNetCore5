using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFundamentos.DTOs;
using WebApiFundamentos.Models;

namespace WebApiFundamentos.Mappers
{
    public class AutorMap : Profile
    {
        public AutorMap()
        {
            CreateMap<Autores, AutorDTO>()
                .ForMember(autorDTO => autorDTO.Id, autor => autor.MapFrom(propiedad => propiedad.Id))
                .ForMember(autorDTO => autorDTO.NombreCompleto, autor => autor.MapFrom(propiedad => propiedad.Nombre));

            CreateMap<Autores, AutorNuevoDTO>()
                .ForMember(autorDTO => autorDTO.NombreAutor, autor => autor.MapFrom(propiedad => propiedad.Nombre))
                .ReverseMap();

            CreateMap<Autores, AutorEditarDTO>()
                .ForMember(autorDTO => autorDTO.Id, autor => autor.MapFrom(propiedad => propiedad.Id))
                .ForMember(autorDTO => autorDTO.Nombre, autor => autor.MapFrom(propiedad => propiedad.Nombre))
                .ReverseMap();
        }
    }
}
